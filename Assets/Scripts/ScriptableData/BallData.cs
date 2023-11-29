using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BallData", menuName = "Data/BallData", order = 2)]
public class BallData : ScriptableObject 
{
    public int currentBallIndex;
    public int damageAmount;
    public float BallSpeed;

    public int MaxBallSpeed=20;
    public int PriceValSpeed;
    public int PriceValDestroyer;
    public int PriceValInvulnerability;

    public bool isItPassed=false;

    //Buffs
    public bool isInvulnerable=false;
    public bool isDestroyer=false;


    internal Rigidbody currentBallRigidbodyData;

    //Shopping
    public int selectedBallIndex;
}
