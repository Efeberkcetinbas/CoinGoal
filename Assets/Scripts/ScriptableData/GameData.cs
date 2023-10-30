using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
public class GameData : ScriptableObject 
{

    public int score;
    public int coin;
    public int increaseScore;
    public int increaseCoinAmount;
    public int LevelNumberIndex;
    public int LevelRequirementNumber;

    //Buff backTime
    public int BackTime;
    
    
    public float ProgressNumber;

    public bool isGameEnd=false;
}
