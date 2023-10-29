using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BallData", menuName = "Data/BallData", order = 2)]
public class BallData : ScriptableObject 
{
    public int currentBallIndex;

    public bool isItPassed=false;
}
