using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Scriptable Data's")]
    public GameData gameData;
    public PlayerData playerData;
    public BallData ballData;

    //Level Progress


    [Header("Game Ending")]
    public GameObject successPanel;
    public GameObject failPanel;
    [SerializeField] private Ease ease;


    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;



    private void Awake() 
    {
        ClearData();
    }
    

    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPassBetween, OnPassBetween);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPassBetween, OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
    }


    
    //DENEME
    public void KillBoss()
    {
        gameData.isGameEnd=true;
        EventManager.Broadcast(GameEvent.OnBossDead);
    }

    public void BossActive()
    {
        EventManager.Broadcast(GameEvent.OnBossActive);
    }

    #region LEVEL PROPERTIES

    //When Level Change Update Req Ball Pass Number
    private void UpdateRequirement()
    {
        gameData.LevelRequirementNumber=FindObjectOfType<RequirementControl>().RequirementNumber;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
        //Update UI
    }

    private void OnPassBetween()
    {
        EventManager.Broadcast(GameEvent.OnIncreaseScore);
        float value=1/(float)gameData.LevelRequirementNumber;
        gameData.ProgressNumber+=value;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
        //Update UI progress bar
    }
  

    #endregion



    void OnGameOver()
    {
        failPanel.SetActive(true);
        failPanel.transform.DOScale(Vector3.one,1f).SetEase(ease);
        playerData.playerCanMove=false;
        gameData.isGameEnd=true;
    }
    private void OnNextLevel()
    {
        gameData.ProgressNumber=0;
        gameData.isGameEnd=true;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
    }

    private void OnGameStart()
    {
        UpdateRequirement();
    }

    
    
    void ClearData()
    {
        ballData.isItPassed=false;
        gameData.isGameEnd=true;
        gameData.ProgressNumber=0;
    }

    public void OpenSuccessMenu(bool station)
    {

        OpenClose(open_close,false);

        successPanel.SetActive(station);
        successPanel.transform.DOScale(Vector2.one*1.15f,0.5f).OnComplete(()=> {
            successPanel.transform.DOScale(Vector2.one,0.5f);
        });
    }

    public void OpenFailMenu()
    {
        failPanel.SetActive(true);
        failPanel.transform.DOScale(Vector2.one*1.15f,0.5f).OnComplete(()=> {
            failPanel.transform.DOScale(Vector2.one,0.5f);
        });
    }

    public void OpenClose(GameObject[] gameObjects,bool canOpen)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(canOpen)
                gameObjects[i].SetActive(true);
            else
                gameObjects[i].SetActive(false);
        }
    }

    
}
