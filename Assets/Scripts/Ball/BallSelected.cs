using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSelected : MonoBehaviour
{
    [SerializeField] private BallData ballData;

    [SerializeField] private List<Transform> ballsTransform=new List<Transform>();

    [SerializeField] private ParticleSystem particle;

    private void Start() 
    {
        OnBallIndexIncrease();
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.AddHandler(GameEvent.OnTouchEnd,OnTouchEnd);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.RemoveHandler(GameEvent.OnTouchEnd,OnTouchEnd);
    }

    private void OnBallIndexIncrease()
    {
        particle.gameObject.SetActive(true);
        particle.transform.position=ballsTransform[ballData.currentBallIndex].position;
        particle.Play();
    }

    private void OnTouchEnd()
    {
        particle.Stop();
        particle.gameObject.SetActive(false);
        
    }

    
}
