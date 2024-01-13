using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GoalTrigger : Obstacleable
{
    [SerializeField] private BallData ballData;
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject wall; 
    [SerializeField] private ParticleSystem goalParticle;

    [SerializeField] private float y,oldy;
    [SerializeField] private GameObject Chest;

    [SerializeField] private Ease ease;

    private WaitForSeconds waitForSeconds;


    private bool isGoal=false;
    public GoalTrigger()
    {
        canStay=false;
    }

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(2);
        
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnMiniGamePasses,OnMiniGamePasses);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnMiniGamePasses,OnMiniGamePasses);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);

    }
    

    internal override void DoAction(Player player)
    {
        if(!isGoal)
        {
            if(ballData.ballsPassTime==3)
            {
                Debug.Log("GOALLLLLL");
                isGoal=true;
                goalParticle.Play();
                gameData.isGameEnd=true;
                //EventManager.Broadcast(GameEvent.OnMiniGameFinish);
                StartCoroutine(OpenChest());
                
            }

            else
            {
                ballData.ballsPassTime=0;
                Debug.Log("3. ATISTA YAPMALISIN!!!!");
                EventManager.Broadcast(GameEvent.OnResetBallsPosition);
            }
        }
    }

    private IEnumerator OpenChest()
    {
        yield return waitForSeconds;
        Chest.SetActive(true);
        Chest.transform.DOPunchScale(Vector3.one,1f).SetEase(ease).OnComplete(()=>EventManager.Broadcast(GameEvent.OnGoal));
    }

    private void OnMiniGamePasses()
    {
        if(ballData.ballsPassTime==3)
            wall.transform.DOLocalMoveY(y,0.5f);
        else
            wall.transform.DOLocalMoveY(oldy,0.1f);

    }

    private void OnRestartLevel()
    {
        ballData.ballsPassTime=0;
        wall.transform.DOLocalMoveY(oldy,0.1f);
    }

    
}
