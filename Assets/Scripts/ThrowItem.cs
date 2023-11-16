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
            float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
            Vector3 touchPosOnScreen = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distanceToCamera);
            Vector3 touchPosInWorld = Camera.main.ScreenToWorldPoint(touchPosOnScreen);
            touchPosInWorld.z = transform.position.z;

            if (Vector3.Distance(transform.position, touchPosInWorld) < 0.05f)
            {
                rb.MovePosition(touchPosInWorld);
            }
            else
            {
                rb.MovePosition(Vector3.Lerp(transform.position, touchPosInWorld, Time.deltaTime * followSpeed));
            }
        }
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
}
