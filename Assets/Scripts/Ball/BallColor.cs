using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : MonoBehaviour
{
    [SerializeField] private BallData ballData;

    [SerializeField] private Material selectedMat,otherMat; 

    private BallController ballController;

    private void Start() 
    {
        ballController=GetComponent<BallController>();
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
    }


    private void OnBallIndexIncrease()
    {
        for (int i = 0; i < ballController.balls.Length; i++)
        {
            ballController.balls[i].GetComponent<MeshRenderer>().material=otherMat;
        }
        ballController.balls[ballData.currentBallIndex].GetComponent<MeshRenderer>().material=selectedMat;
    }
}
