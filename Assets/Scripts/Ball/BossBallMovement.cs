using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossBallMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;


    [SerializeField] private LineRenderer powerIndicator;
    public Material lineMaterial;

    private Rigidbody rb;




    public float minPower=1f;
    

    public float maxLineLength=2f;

    private Camera cam;

    void Start()
    {
        
        powerIndicator.positionCount = 2;
        powerIndicator.useWorldSpace = true;
        powerIndicator.material = lineMaterial;

        cam=Camera.main;
        rb=GetComponent<Rigidbody>();
    }   
    private void Update() 
    {
        Movement();
    }
    private void Movement()
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
                        break;

                    case TouchPhase.Moved:
                        touchEndPos = touch.position;
                        UpdatePowerIndicator();
                        
                        break;

                    case TouchPhase.Ended:
                        if (isDragging)
                        {
                            Vector3 dragDirection = touchEndPos - touchStartPos;
                            float dragDistance = dragDirection.magnitude;
                            Debug.Log("DRAG DISTANCE: " + dragDistance);
                            //0.05f
                            float forceMagnitude = Mathf.Clamp(dragDistance * 0.02f, minPower, 25);
                            Vector3 force = new Vector3(dragDirection.x, 0f, dragDirection.y).normalized*forceMagnitude;
                            //Inverse Drag Not Swipe You Must do -force                            
                            rb.AddForce(-force, ForceMode.Impulse);
                            isDragging = false;
                            powerIndicator.startColor = Color.green;
                            powerIndicator.endColor = Color.green;
                            Debug.Log("FORCE MAG " + (int)forceMagnitude);
                            transform.DOScale(new Vector3(1.3f,1.3f,1),0.15f).OnComplete(()=> transform.DOScale(Vector3.one,0.15f));
                            

                            powerIndicator.SetPosition(0, Vector3.zero);
                            powerIndicator.SetPosition(1, Vector3.zero);
                            //powerIndicate.localScale=new Vector3(1,1,1);
                        }
                        break;
                }
            }
    }

    void UpdatePowerIndicator()
    {
        Vector3 worldStartPos = cam.ScreenToWorldPoint(new Vector3(touchStartPos.x, touchStartPos.y, 10f));
        Vector3 worldEndPos = cam.ScreenToWorldPoint(new Vector3(touchEndPos.x, touchEndPos.y, 10f));

        Vector3 dragDirection = worldEndPos - worldStartPos;
        dragDirection.y=0;
        float dragDistance = dragDirection.magnitude;

        // Clamp the Line Renderer length based on maxLineLength.
        if (dragDistance > maxLineLength)
        {
            dragDirection = dragDirection.normalized * maxLineLength;
        }

        float normalizedDistance = Mathf.Clamp(dragDistance * 1/maxLineLength, 0f, 1f);

        powerIndicator.SetPosition(0, transform.localPosition);
        powerIndicator.SetPosition(1, transform.localPosition+dragDirection);

        // Update the Line Renderer color based on the normalized distance.
        Color color = Color.Lerp(Color.green, Color.red, normalizedDistance);
        lineMaterial.color = color; 

        

    }
}
