using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallSelected : MonoBehaviour
{
    [SerializeField] private BallData ballData;

    [SerializeField] private List<Transform> ballsTransform=new List<Transform>();

    [SerializeField] private ParticleSystem particle;

    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.AddHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.AddHandler(GameEvent.OnMiniGameFinish,OnMiniGameFinish);
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.RemoveHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.RemoveHandler(GameEvent.OnMiniGameFinish,OnMiniGameFinish);
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
    }

    private void OnBallIndexIncrease()
    {
        particle.gameObject.SetActive(true);
        particle.transform.position=ballsTransform[ballData.currentBallIndex].position;
        //particle.transform.rotation=quaternion.identity;
        particle.Play();
    }

    private void OnGameStart()
    {
        OnBallIndexIncrease();
    }

    private void OnTouchEnd()
    {
        particle.Stop();
        particle.gameObject.SetActive(false);
        
    }

    

    private void OnMiniGameFinish()
    {
        particle.gameObject.SetActive(false);
    }

    
}
