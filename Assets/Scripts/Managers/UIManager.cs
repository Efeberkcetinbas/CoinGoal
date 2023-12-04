using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text's")]
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI fromLevelText;
    [SerializeField] private TextMeshProUGUI toLevelText;
    [SerializeField] private TextMeshProUGUI diamondText;

    [Header("Image's")]
    [SerializeField] private Image progressBar;
    [SerializeField] private Image buffProgressBar;

    [Header("Mini Game")]
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private Image counterProgressBar;

    
    
    [Header("Data's")]
    public GameData gameData;
    public PlayerData playerData;
    public BallData ballData;


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        EventManager.AddHandler(GameEvent.OnUpdateBuff,OnUpdateBuff);
        EventManager.AddHandler(GameEvent.OnUIDiamondUpdate,OnUIDiamondUpdate);
        EventManager.AddHandler(GameEvent.OnMiniGameUIUpdate,OnMiniGameUIUpdate);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        EventManager.RemoveHandler(GameEvent.OnUpdateBuff,OnUpdateBuff);
        EventManager.RemoveHandler(GameEvent.OnUIDiamondUpdate,OnUIDiamondUpdate);
        EventManager.RemoveHandler(GameEvent.OnMiniGameUIUpdate,OnMiniGameUIUpdate);
    }

    private void Start() 
    {
        OnNextLevel();
        OnUIUpdate();
    }
    
    private void OnUIUpdate()
    {
        score.SetText(gameData.score.ToString());
        score.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
    }

    private void OnUIDiamondUpdate()
    {
        diamondText.SetText(gameData.diamond.ToString());
    }


    private void OnNextLevel()
    {
        fromLevelText.SetText((gameData.LevelNumberIndex).ToString());
        toLevelText.SetText((gameData.LevelNumberIndex+1).ToString());
    }

    private void OnUIRequirementUpdate()
    {
        progressBar.DOFillAmount(gameData.ProgressNumber,0.25f);
    }

    private void OnUpdateBuff()
    {
        buffProgressBar.DOFillAmount(0,gameData.BackTime+1).OnComplete(()=>buffProgressBar.gameObject.SetActive(false));
    }

    private void OnMiniGameUIUpdate()
    {
        counterText.SetText(ballData.ballsPassTime.ToString());
        counterText.transform.localScale=Vector3.zero;
        counterText.transform.DOScale(Vector3.one*1.5f,0.5f).OnComplete(()=>counterText.transform.DOScale(Vector3.zero,0.5f));
        float val=ballData.ballsPassTime/3;
        counterProgressBar.DOFillAmount(val,0.25f);
    }

    

    


    
}
