using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowItem : MonoBehaviour
{
    [Header("Throw Settings")]
    [SerializeField] private float maxThrowForce = 50f;
    [SerializeField] private float minSwipeThreshold = 10f;
    private float maxSwipeDistance = 200f;

    [Header("Drag Settings")]
    [SerializeField] private float followSpeed = 5f;

    private Vector2 startSwipePos;
    private Vector2 endSwipePos;
    private bool isDragging = false;
    private bool isThrown = false;
    private Vector2 lastTouchPos;
    public static event Action OnBallThrown;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        maxSwipeDistance = Screen.height * 0.45f;
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        HandleDrag();
    }

    private void HandleInput()
    {
        if (isThrown)
            return;

        if (Time.timeScale == 0 || !PauseManager.canThrow)
            return;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

                startSwipePos = touch.position;
                isDragging = true;
                rb.useGravity = false;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endSwipePos = touch.position;
                isDragging = false;

                if (Vector2.Distance(startSwipePos, endSwipePos) > minSwipeThreshold)
                {
                    rb.useGravity = true;
                    ThrowBall();
                    PauseManager.canThrow = false;
                    isThrown = true;
                }
            }
        }
    }

    private void HandleDrag()
    {
        if (isThrown)
            return;

        if (isDragging)
        {
            Touch touch = Input.GetTouch(0);

            float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
            Vector3 touchPosOnScreen = new Vector3(touch.position.x, touch.position.y, distanceToCamera);
            Vector3 touchPosInWorld = Camera.main.ScreenToWorldPoint(touchPosOnScreen);
            touchPosInWorld.z = transform.position.z;

            if (Vector3.Distance(transform.position, touchPosInWorld) < 0.05f)
            {
                RotateObject(touch.position);
                rb.MovePosition(touchPosInWorld);
            }
            else
            {
                RotateObject(touch.position);
                rb.MovePosition(Vector3.Lerp(transform.position, touchPosInWorld, Time.deltaTime * followSpeed));
            }
        }
        else
        {
            lastTouchPos = Vector2.zero;
        }
    }

    private void RotateObject(Vector2 currentTouchPos)
    {
        if (lastTouchPos != Vector2.zero)
        {
            // Calculate the circular movement
            Vector2 deltaPos = currentTouchPos - lastTouchPos;
            float rotationAmount = Mathf.Atan2(deltaPos.y, deltaPos.x) * Mathf.Rad2Deg;

            // Adjust the rotation speed
            float rotationSpeed = 0.2f;

            // Use AddTorque to apply torque for rotation
            rb.AddTorque(Vector3.forward * rotationAmount * rotationSpeed);
        }

        lastTouchPos = currentTouchPos;
    }

    private void ThrowBall()
    {
        Vector2 swipeDir = endSwipePos - startSwipePos;

        float normalizedSwipeLength = Mathf.Clamp01(swipeDir.magnitude / maxSwipeDistance);
        float throwForce = normalizedSwipeLength * maxThrowForce;

        Vector3 forceDirection = new Vector3(swipeDir.x, swipeDir.y, 0).normalized;
        forceDirection = Camera.main.transform.TransformDirection(forceDirection);
        forceDirection.z = 1;

        rb.AddForce(forceDirection * throwForce, ForceMode.Impulse);
        OnBallThrown?.Invoke();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Treadmill"))
        {
            // Nollaa nykyinen nopeus ja rotaatio
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Halutut voimakomponentit x ja z suuntiin
            float desiredXForce = -100.0f; // Voima x-suunnassa
            float desiredZForce = 100.0f;  // Voima z-suunnassa

            // Lisää voima halutuilla komponenteilla
            rb.AddForce(new Vector3(desiredXForce, 0f, desiredZForce), ForceMode.Impulse);
        }
    }



}
