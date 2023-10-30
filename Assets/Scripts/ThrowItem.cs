using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowItem : MonoBehaviour
{
    [Header("Throw Settings")]
    [SerializeField]
    private float maxThrowForce = 50f;
    [SerializeField]
    private float minSwipeThreshold = 10f; // Minimum swipe distance to count as a throw
    
    private float maxSwipeDistance = 200f; // Maximum swipe distance to count for max force


    [Header("Drag Settings")]
    [SerializeField]
    private float followSpeed = 5f;

    private Vector2 startSwipePos;
    private Vector2 endSwipePos;
    private bool isDragging = false;
    public static event Action OnBallThrown;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        maxSwipeDistance = Screen.height * 0.65f;
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
        // Check if there's at least one touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began)
            {
                startSwipePos = touch.position;
                isDragging = true;
                rb.useGravity = false;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endSwipePos = touch.position;
                isDragging = false;
                rb.useGravity = true;
                if (Vector2.Distance(startSwipePos, endSwipePos) > minSwipeThreshold)
                {
                    ThrowBall();
                }
            }
        }
    }

    private void HandleDrag()
    {
        if (isDragging)
        {
            float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
            Vector3 touchPosOnScreen = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distanceToCamera);
            Vector3 touchPosInWorld = Camera.main.ScreenToWorldPoint(touchPosOnScreen);
            touchPosInWorld.z = transform.position.z;

            // Check if the ball is close enough to the touch position
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

        // Convert 2D swipe direction into 3D force direction
        Vector3 forceDirection = new Vector3(swipeDir.x, swipeDir.y, 0).normalized;

        // Rotate this force direction from screen space into world space.
        forceDirection = Camera.main.transform.TransformDirection(forceDirection);
        forceDirection.z = 1;  // Ensures forward motion along the z-axis.

        rb.AddForce(forceDirection * throwForce, ForceMode.Impulse);
        OnBallThrown?.Invoke();
    }

}
