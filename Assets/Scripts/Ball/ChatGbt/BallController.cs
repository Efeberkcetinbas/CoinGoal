using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    public GameData gameData;
    private Rigidbody currentBallRigidbody;

    [SerializeField] private LineRenderer powerIndicator;
    public Material lineMaterial;




    public float minPower=1f;
    

    public float maxLineLength=2f;


    /*
    
    
        private void Start() 
    {
        line.positionCount=2;
    }
    //Bu scripte gerek yok ama rendereri dogru olcmek icin dogrulama amacli kullaniyorum. Ilerde scripti kaldiririm.
    private void Update() 
    {
        line.SetPosition(0,ball1.position);
        line.SetPosition(1,ball2.position);

    }
    
    */
    
    

    void Start()
    {
        currentBallRigidbody=balls[ballData.currentBallIndex].GetComponent<Rigidbody>();
        ballData.currentBallRigidbodyData=currentBallRigidbody;
        for (int i = 0; i < BallLines.Count; i++)
        {
            BallLines[i].SetActive(false);
        }

        BallLines[ballData.currentBallIndex].SetActive(true);
        
        powerIndicator.positionCount = 2;
        powerIndicator.useWorldSpace = true;
        powerIndicator.material = lineMaterial;

        OnNextLevel();

        
    }   

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.AddHandler(GameEvent.OnDamagePlayer,OnDamagePlayer);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnDamagePlayer,OnDamagePlayer);
        
    }

    private void Update() 
    {
        if(!gameData.isGameEnd)
            MoveBalls();
    }
    private void MoveBalls()
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
                        currentBallRigidbody.transform.DOScale(Vector3.one/1.15f,0.3f);
                        if(isTurn)
                        {
                            ballData.currentBallIndex++;
                            
                            if (ballData.currentBallIndex >= balls.Length)
                            {
                                ballData.currentBallIndex = 0; // Reset to the first ball once all balls have been used.
                            }

                            currentBallRigidbody = balls[ballData.currentBallIndex].GetComponent<Rigidbody>();
                            ballData.currentBallRigidbodyData=currentBallRigidbody;
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
                            Debug.Log("DRAG DISTANCE: " + dragDistance);
                            //0.05f
                            float forceMagnitude = Mathf.Clamp(dragDistance * 0.02f, minPower, ballData.MaxBallSpeed);
                            Vector3 force = new Vector3(dragDirection.x, 0f, dragDirection.y).normalized*forceMagnitude;
                            //Inverse Drag Not Swipe You Must do -force                            
                            currentBallRigidbody.AddForce(-force, ForceMode.Impulse);
                            isDragging = false;
                            powerIndicator.startColor = Color.green;
                            powerIndicator.endColor = Color.green;
                            Debug.Log("FORCE MAG " + (int)forceMagnitude);
                            currentBallRigidbody.transform.DOScale(new Vector3(1.3f,1.3f,1),0.15f).OnComplete(()=> currentBallRigidbody.transform.DOScale(Vector3.one,0.15f));
                            
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

        ballData.BallSpeed=currentBallRigidbody.velocity.magnitude;
    

    }

    private void OnDamagePlayer()
    {
        currentBallRigidbody.isKinematic=true;
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

        powerIndicator.SetPosition(0, currentBallRigidbody.position);
        powerIndicator.SetPosition(1, currentBallRigidbody.position+dragDirection);

        // Update the Line Renderer color based on the normalized distance.
        Color color = Color.Lerp(Color.green, Color.red, normalizedDistance);
        lineMaterial.color = color; 

        

    }

    private void OnNextLevel()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].GetComponent<SphereCollider>().isTrigger=false;
            balls[i].GetComponent<Rigidbody>().useGravity=true;
            balls[i].transform.position=FindObjectOfType<BallPositions>().PositionsOfBall[i];
        }
        currentBallRigidbody.isKinematic=false;
    }

    private void OnGameStart()
    {
        isTurn=false;
    }

    private void OnPortalOpen()
    {
        StartCoroutine(MoveBallsJump());
    }

    private IEnumerator MoveBallsJump()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].GetComponent<SphereCollider>().isTrigger=true;
            balls[i].GetComponent<Rigidbody>().useGravity=false;
            balls[i].transform.DOJump(FindObjectOfType<PortalControl>().PortalPosition,2,4,3);

            yield return new WaitForSeconds(i+2);
        }

        EventManager.Broadcast(GameEvent.OnLoadNextLevel);
    }


    
}
