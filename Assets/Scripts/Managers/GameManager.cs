using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public PlayerData playerData;
    public BallData ballData;

    [SerializeField] private GameObject FailPanel;
    [SerializeField] private Ease ease;

    public float InitialDifficultyValue;

    [Header("Game Ending")]
    public GameObject successPanel;
    public GameObject failPanel;

    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;

    
    //Daha iyisi olana kadar
    [Header("Line Collisions")]
    public List<GameObject> LinesCol=new List<GameObject>(); 
    public bool canCollide=false;
    public bool success=false;
    public bool isWall=false;



    private void Awake() 
    {
        ClearData();
    }

    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
    }
    public void LineOpenControl(int selected)
    {
        for (int i = 0; i < LinesCol.Count; i++)
        {
            LinesCol[i].SetActive(false);
        }

        LinesCol[selected].SetActive(true);
    }

    void OnGameOver()
    {
        FailPanel.SetActive(true);
        FailPanel.transform.DOScale(Vector3.one,1f).SetEase(ease);
        playerData.playerCanMove=false;
        gameData.isGameEnd=true;

    }
    

    void OnIncreaseScore()
    {
        //gameData.score += 50;
        DOTween.To(GetScore,ChangeScore,gameData.score+gameData.increaseScore,1f).OnUpdate(UpdateUI);
    }

    private int GetScore()
    {
        return gameData.score;
    }

    private void ChangeScore(int value)
    {
        gameData.score=value;
    }

    private void UpdateUI()
    {
        EventManager.Broadcast(GameEvent.OnUIUpdate);
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
