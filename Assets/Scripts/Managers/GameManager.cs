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
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPassBetween, OnPassBetween);
    }


    private void OnPassBetween()
    {
        EventManager.Broadcast(GameEvent.OnIncreaseScore);
    }
    

    void OnGameOver()
    {
        failPanel.SetActive(true);
        failPanel.transform.DOScale(Vector3.one,1f).SetEase(ease);
        playerData.playerCanMove=false;
        gameData.isGameEnd=true;
    }
    

    
    
    void ClearData()
    {
        ballData.isItPassed=false;
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
