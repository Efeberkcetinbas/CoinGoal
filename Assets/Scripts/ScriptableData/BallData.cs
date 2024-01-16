using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BallData", menuName = "Data/BallData", order = 2)]
public class BallData : ScriptableObject 
{
    public int currentBallIndex;
    public int damageAmount;
    public float BallSpeed;
    public float ballsPassTime;
    public int hitBarrel;

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

    public void SaveData()
    {
        PlayerPrefs.SetInt("SelectedBallIndex",selectedBallIndex);
        PlayerPrefs.SetInt("PriceValSpeed",PriceValSpeed);
        PlayerPrefs.SetInt("PriceValDestroyer",PriceValDestroyer);
        PlayerPrefs.SetInt("PriceValInvulnerability",PriceValInvulnerability);
    }

    public void LoadData()
    {
        selectedBallIndex=PlayerPrefs.GetInt("SelectedBallIndex");
        PriceValSpeed=PlayerPrefs.GetInt("PriceValSpeed",20);
        PriceValDestroyer=PlayerPrefs.GetInt("PriceValDestroyer",75);
        PriceValInvulnerability=PlayerPrefs.GetInt("PriceValInvulnerability",50);
    }
}
