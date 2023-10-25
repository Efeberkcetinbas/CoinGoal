using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallControl : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;


    public Rigidbody2D rb;

    private Vector3 dragStartPos;
    private Touch touch;

    public BallManager ballManager;
    public LineRendererManager lrManager;

    public bool canShoot=false;

    private Camera cm;

    
    private GameManager gameManager;
    private LevelManager levelManager;

    private void Start()
    {
        cm=Camera.main;
        ballManager=FindObjectOfType<BallManager>();
        lrManager=FindObjectOfType<LineRendererManager>();
        gameManager=GameManager.Instance;
        levelManager=LevelManager.Instance;
    }
    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,StopSpinning);
        EventManager.AddHandler(GameEvent.OnBossDie,OnBossDie);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,StopSpinning);
        EventManager.RemoveHandler(GameEvent.OnBossDie,OnBossDie);
    }

    private void StopSpinning()
    {
        rb.constraints=RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnBossDie()
    {
        rb.constraints=RigidbodyConstraints2D.FreezeRotation;
    }


    private void Update()
    {
        if(!GameManager.Instance.isGameEnd)
        {
            if(canShoot)
                DragControl();
        }
            
    }

    

    private void DragControl()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                DragRealease();
            }
        }
    }
    private void DragStart()
    {
        dragStartPos = cm.ScreenToWorldPoint(touch.position);
        rb.constraints=RigidbodyConstraints2D.None;
        dragStartPos.z = 0f;
        lrManager.lr.positionCount = 1;
        lrManager.lr.SetPosition(0, dragStartPos);
        gameManager.canCollide=false;
        gameManager.LineOpenControl(ballManager.index);
        transform.DOScale(new Vector3(.4f,.4f,.4f),0.2f);
        //Pushlamak icin
    }
    private void Dragging()
    {
        Vector3 draggingPos = cm.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lrManager.lr.positionCount = 2;
        lrManager.lr.SetPosition(1, draggingPos);
    }
    private void DragRealease()
    {
        lrManager.lr.positionCount = 0;

        Vector3 dragReleasePos = cm.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        GameManager.Instance.isWall=false;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        transform.DOScale(new Vector3(.5f,.5f,.5f),0.2f);
        gameManager.canCollide=true;
        EventManager.Broadcast(GameEvent.OnFingerRelease);

        

        
        
        StartCoroutine(Call());
    }

    private IEnumerator Call()
    {
        yield return null;
        CallBallManager();
    }

    private void CallBallManager()
    {
        ballManager.IncreaseIndex();
        ballManager.CheckIndex();
        ballManager.OpenSignal();

        /*
        Added For Trying Progress and Requirement
        */
        
    }


}
