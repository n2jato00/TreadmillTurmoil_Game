using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCurve : MonoBehaviour
{
    private Vector2 startSwipePos;
    private Vector2 lastSwipePos;
    private bool isDragging = false;

    private Rigidbody rb;

    public float followSpeed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startSwipePos = Input.mousePosition;
            isDragging = true;
            rb.useGravity = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.useGravity = true;
            ThrowBall();
        }

        lastSwipePos = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            mousePosInWorld.z = transform.position.z;

            rb.MovePosition(Vector3.Lerp(transform.position, mousePosInWorld, Time.deltaTime * followSpeed));
        }
    }

    void ThrowBall()
    {
        Vector2 swipeDir = (Vector2)Input.mousePosition - startSwipePos;
        float swipeDistance = swipeDir.magnitude;
        float swipeHorizontalPercentage = Mathf.Abs(swipeDir.x) / swipeDistance;

        float curveAmount = 0;

        // Only consider curve if the horizontal movement is more than, say, 30% of the total swipe
        if (swipeHorizontalPercentage > 0.3f)
        {
            curveAmount = swipeDir.x / Screen.width;
        }

        // Adjust forward force by swipe distance and curve by curveAmount
        Vector3 forceDirection = new Vector3(curveAmount * 10, 1, 1).normalized;

        rb.AddForce(forceDirection * swipeDistance * 2, ForceMode.Impulse);
    }


}
