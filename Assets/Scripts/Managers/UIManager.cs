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

    [Header("Image's")]
    [SerializeField] private Image progressBar;
    [SerializeField] private Image buffProgressBar;

    
    
    [Header("Data's")]
    public GameData gameData;
    public PlayerData playerData;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        EventManager.AddHandler(GameEvent.OnUpdateBuff,OnUpdateBuff);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        EventManager.RemoveHandler(GameEvent.OnUpdateBuff,OnUpdateBuff);
    }

    private void Start() 
    {
        OnNextLevel();
    }
    
    private void OnUIUpdate()
    {
        score.SetText(gameData.score.ToString());
        score.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
    }

    private void OnNextLevel()
    {
        fromLevelText.SetText((gameData.LevelNumberIndex+1).ToString());
        toLevelText.SetText((gameData.LevelNumberIndex+2).ToString());
    }

    private void OnUIRequirementUpdate()
    {
        progressBar.DOFillAmount(gameData.ProgressNumber,0.25f);
    }

    private void OnUpdateBuff()
    {
        buffProgressBar.DOFillAmount(0,gameData.BackTime).OnComplete(()=>buffProgressBar.gameObject.SetActive(false));
    }

    


    
}
