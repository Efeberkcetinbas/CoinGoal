using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BallColor : MonoBehaviour
{
    [SerializeField] private BallData ballData;

    [SerializeField] private Material selectedMat,otherMat; 

    private BallController ballController;

    private void Start() 
    {
        ballController=GetComponent<BallController>();
        OnBallIndexIncrease();
        
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }
    

    private void OnBallIndexIncrease()
    {
        ChangeMatColor(Color.white,Color.green);
    }

    private void OnPortalOpen()
    {
        ChangeMatColor(Color.white,Color.white);
    }


    private void ChangeMatColor(Color color1,Color color2)
    {
        for (int i = 0; i < ballController.balls.Length; i++)
        {
            ballController.balls[i].GetComponent<MeshRenderer>().material.color=color1;
        }
        ballController.balls[ballData.currentBallIndex].GetComponent<MeshRenderer>().material.color=color2;
    }
}
