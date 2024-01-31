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

    //Price
    public int priceForIncreaseScore;
    public int priceForIncreaseDiamond;
    public int priceForBuffTime;
    
    //Level Property
    public int LevelNumberIndex=1;
    public int LevelRequirementNumber;
    public int BorderIndex;
    public int levelProgressNumber;
    public int skyboxIndex;
    public int tempLevelIndex;

    

    //Buff
    public int BuffTime;
    
    
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
        PlayerPrefs.SetInt("skyboxIndex",skyboxIndex);
        PlayerPrefs.SetInt("increaseScore",increaseScore);
        PlayerPrefs.SetInt("increaseCoinAmount",increaseCoinAmount);
        PlayerPrefs.SetInt("BuffTime",BuffTime);

        //Pricelari da set Et 
        
    }


    public void LoadData()
    {
        score=PlayerPrefs.GetInt("Score",0);
        diamond=PlayerPrefs.GetInt("Diamond",0);
        skyboxIndex=PlayerPrefs.GetInt("skyboxIndex");
        LevelNumberIndex=PlayerPrefs.GetInt("LevelNumberIndex",0);
        increaseScore=PlayerPrefs.GetInt("increaseScore",5);
        increaseCoinAmount=PlayerPrefs.GetInt("increaseCoinAmount",5);
        BuffTime=PlayerPrefs.GetInt("BuffTime",5);
        
    }

}
