using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GoalTrigger : Obstacleable
{
    [SerializeField] private BallData ballData;
    [SerializeField] private GameObject wall; 
    [SerializeField] private ParticleSystem goalParticle;

    [SerializeField] private float y,oldy;
    [SerializeField] private GameObject Chest;


    private bool isGoal=false;
    public GoalTrigger()
    {
        canStay=false;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnMiniGamePasses,OnMiniGamePasses);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnMiniGamePasses,OnMiniGamePasses);
        
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
                //EventManager.Broadcast(GameEvent.OnMiniGameFinish);

                Chest.SetActive(true);
                Chest.transform.DOPunchScale(Vector3.one,0.5f).OnComplete(()=>EventManager.Broadcast(GameEvent.OnGoal));
            }

            else
            {
                ballData.ballsPassTime=0;
                Debug.Log("3. ATISTA YAPMALISIN!!!!");
                EventManager.Broadcast(GameEvent.OnResetBallsPosition);
            }
        }
    }

    private void OnMiniGamePasses()
    {
        if(ballData.ballsPassTime==3)
            wall.transform.DOLocalMoveY(y,0.1f);
        else
            wall.transform.DOLocalMoveY(oldy,0.1f);

    }

    
}
