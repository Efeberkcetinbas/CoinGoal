using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallSelected : MonoBehaviour
{
    [SerializeField] private BallData ballData;

    [SerializeField] private List<Player> ballsTransform=new List<Player>();


    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.AddHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.AddHandler(GameEvent.OnGoal,OnGoal);
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.RemoveHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.RemoveHandler(GameEvent.OnGoal,OnGoal);
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }

    private void OnBallIndexIncrease()
    {
        OpenClose(true);
    }

    private void OnGameStart()
    {
        OnBallIndexIncrease();
    }

    private void OnTouchEnd()
    {
        OpenClose(false);
        
    }

    private void OnPortalOpen()
    {
        OpenClose(false);
    }

    

    private void OnGoal()
    {
        OpenClose(false);
    }

    private void OpenClose(bool val)
    {
        ballsTransform[ballData.currentBallIndex].selectedParticle.gameObject.SetActive(val);
        //ballsTransform[ballData.currentBallIndex].selectedParticle.transform.position=ballsTransform[ballData.currentBallIndex].transform.position;
    }

    
}
