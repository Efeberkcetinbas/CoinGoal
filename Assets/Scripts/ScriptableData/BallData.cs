using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BallData", menuName = "Data/BallData", order = 2)]
public class BallData : ScriptableObject 
{
    public int currentBallIndex;
    public float BallSpeed;

    public int MaxBallSpeed=20;

    public bool isItPassed=false;

    //Buffs
    public bool isInvulnerable=false;


    internal Rigidbody currentBallRigidbodyData;
}
