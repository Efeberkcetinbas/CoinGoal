using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{   
    public GameObject[] balls;
    private bool isDragging = false;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    private int currentBallIndex=0;
    private Rigidbody currentBallRigidbody;

    public float power = 10f;

    void Start()
    {
        currentBallRigidbody=balls[currentBallIndex].GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(currentBallIndex<balls.Length)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isDragging = true;
                        touchStartPos = touch.position;
                        break;

                    case TouchPhase.Moved:
                        touchEndPos = touch.position;
                        break;

                    case TouchPhase.Ended:
                        if (isDragging)
                        {
                            Vector3 dragDirection = (touchEndPos - touchStartPos).normalized;
                            Vector3 force = new Vector3(dragDirection.x, 0f, dragDirection.y) * power;
                            currentBallRigidbody.AddForce(force, ForceMode.Impulse);
                            isDragging = false;
                            currentBallIndex++;

                            if (currentBallIndex >= balls.Length)
                            {
                                currentBallIndex = 0; // Reset to the first ball once all balls have been used.
                            }
                            currentBallRigidbody = balls[currentBallIndex].GetComponent<Rigidbody>();
                        }
                        break;
                }
            }
        }
    }
}
