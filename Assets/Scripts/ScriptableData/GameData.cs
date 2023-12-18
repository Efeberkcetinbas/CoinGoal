using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
public class GameData : ScriptableObject 
{

    //Level Score and Coin
    public int score;
    public int diamond;
    public int increaseScore;
    public int increaseCoinAmount;
    
    //Level Property
    public int LevelNumberIndex=1;
    public int LevelRequirementNumber;
    public int BorderIndex;
    public int levelProgressNumber;

    

    //Buff
    public int BackTime;
    
    
    //UI Progress 
    public float ProgressNumber;

    //Game Management
    public bool isGameEnd=false;
    public bool isMiniGame=false;
    public bool canChangeIndex=true;
    public bool canIntersect=true;

    public void SaveData()
    {
        PlayerPrefs.SetInt("Score",score);
        PlayerPrefs.SetInt("Diamond",diamond);
        PlayerPrefs.SetInt("LevelNumberIndex",LevelNumberIndex);
    }

    public void LoadData()
    {
        score=PlayerPrefs.GetInt("Score");
        diamond=PlayerPrefs.GetInt("Diamond");
        //LevelNumberIndex=PlayerPrefs.GetInt("LevelNumberIndex",1);
    }

}
