using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{   
    public GameObject[] balls;
    public List<GameObject> BallLines= new List<GameObject>();
    private bool isDragging = false;
    private bool isTurn=false;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    public BallData ballData;
    private Rigidbody currentBallRigidbody;

    [SerializeField] private LineRenderer powerIndicator;
    public Material lineMaterial;


    public Transform powerIndicate;


    public float minPower=1f;
    public float maxPower=20f;

    public float maxLineLength=2f;


    
    

    void Start()
    {
        currentBallRigidbody=balls[ballData.currentBallIndex].GetComponent<Rigidbody>();
        for (int i = 0; i < BallLines.Count; i++)
        {
            BallLines[i].SetActive(false);
        }

        BallLines[ballData.currentBallIndex].SetActive(true);
        
        powerIndicator.positionCount = 2;
        powerIndicator.useWorldSpace = true;
        powerIndicator.material = lineMaterial;
    }

    void Update()
    {
        if(ballData.currentBallIndex<balls.Length)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isDragging = true;
                        touchStartPos = touch.position;
                        //Otherwise It Moves With Max Speed
                        touchEndPos=touch.position;
                        
                        EventManager.Broadcast(GameEvent.OnTouchStart);

                        if(isTurn)
                        {
                            ballData.currentBallIndex++;
                            
                            if (ballData.currentBallIndex >= balls.Length)
                            {
                                ballData.currentBallIndex = 0; // Reset to the first ball once all balls have been used.
                            }

                            currentBallRigidbody = balls[ballData.currentBallIndex].GetComponent<Rigidbody>();
                            currentBallRigidbody.isKinematic=true;
                            balls[ballData.currentBallIndex].transform.rotation=Quaternion.identity;
                            for (int i = 0; i < BallLines.Count; i++)
                            {
                                BallLines[i].SetActive(false);
                            }

                            BallLines[ballData.currentBallIndex].SetActive(true);
                            EventManager.Broadcast(GameEvent.OnBallIndexIncrease);
                            ballData.isItPassed=false;
                        }
                        break;

                    case TouchPhase.Moved:
                        touchEndPos = touch.position;
                        currentBallRigidbody.isKinematic=false;
                        UpdatePowerIndicator();
                        break;

                    case TouchPhase.Ended:
                        if (isDragging)
                        {
                            Vector3 dragDirection = touchEndPos - touchStartPos;
                            float dragDistance = dragDirection.magnitude;
                            float forceMagnitude = Mathf.Clamp(dragDistance * 0.05f, minPower, maxPower);
                            Vector3 force = new Vector3(dragDirection.x, 0f, dragDirection.y).normalized*forceMagnitude;
                            //Inverse Drag Not Swipe You Must do -force                            
                            currentBallRigidbody.AddForce(-force, ForceMode.Impulse);
                            isDragging = false;
                            powerIndicator.startColor = Color.green;
                            powerIndicator.endColor = Color.green;
                            Debug.Log("FORCE MAG " + (int)forceMagnitude);
                            
                            //Ball is Change when I touch to start
                            isTurn=true;

                            powerIndicator.SetPosition(0, Vector3.zero);
                            powerIndicator.SetPosition(1, Vector3.zero);
                            //powerIndicate.localScale=new Vector3(1,1,1);
                            EventManager.Broadcast(GameEvent.OnTouchEnd);

                            
                        }
                        break;
                }
            }
        }
    

    }

   

    void UpdatePowerIndicator()
    {
        Vector3 worldStartPos = Camera.main.ScreenToWorldPoint(new Vector3(touchStartPos.x, touchStartPos.y, 10f));
        Vector3 worldEndPos = Camera.main.ScreenToWorldPoint(new Vector3(touchEndPos.x, touchEndPos.y, 10f));

        Vector3 dragDirection = worldEndPos - worldStartPos;
        dragDirection.y=0;
        float dragDistance = dragDirection.magnitude;

        // Clamp the Line Renderer length based on maxLineLength.
        if (dragDistance > maxLineLength)
        {
            dragDirection = dragDirection.normalized * maxLineLength;
        }

        float normalizedDistance = Mathf.Clamp(dragDistance / maxLineLength, 0f, 1f);

        powerIndicator.SetPosition(0, worldStartPos);
        powerIndicator.SetPosition(1, worldStartPos + dragDirection);

        // Update the Line Renderer color based on the normalized distance.
        Color color = Color.Lerp(Color.green, Color.red, normalizedDistance);
        lineMaterial.color = color;

        /*float arrowLength=Vector3.Distance(worldEndPos,worldStartPos);
        powerIndicate.localScale=new Vector3(1,1,arrowLength);*/
    }

    
}
