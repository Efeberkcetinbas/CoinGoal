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

    [Header("Boss")]
    [SerializeField] private RectTransform bossImage;
    [SerializeField] private TextMeshProUGUI bossText;
    [SerializeField] private Image bossProgressBar;

    [SerializeField] private GameObject BossUI;

    [SerializeField] private float y_axis;


    [SerializeField] private GameObject[] bars;

    
    
    [Header("Data's")]
    public GameData gameData;
    public PlayerData playerData;
    public BossData bossData;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        EventManager.AddHandler(GameEvent.OnUpdateBuff,OnUpdateBuff);
        EventManager.AddHandler(GameEvent.OnBossActive,OnBossActive);
        EventManager.AddHandler(GameEvent.OnUIBossUpdate,OnUpdateUIBoss);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnUIRequirementUpdate,OnUIRequirementUpdate);
        EventManager.RemoveHandler(GameEvent.OnUpdateBuff,OnUpdateBuff);
        EventManager.RemoveHandler(GameEvent.OnBossActive,OnBossActive);
        EventManager.RemoveHandler(GameEvent.OnUIBossUpdate,OnUpdateUIBoss);
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

    private void OnBossActive()
    {
        //UI Managerda tut bunlari
        bossData=FindObjectOfType<BossControl>().bossData;
        BossUI.SetActive(true);
        BossUI.transform.DOScale(Vector3.one*2,1f).OnComplete(()=>{
            BossUI.transform.DOScale(Vector3.one,1f).OnComplete(()=>{
                bossImage.DOAnchorPosY(y_axis,1);
                for (int i = 0; i < bars.Length; i++)
                {
                    bars[i].SetActive(true);
                }
                bossProgressBar.fillAmount=0;
                bossProgressBar.DOFillAmount(1,1);
            });
        });
        bossText.SetText(bossData.Name);
    }

    private void OnUpdateUIBoss()
    {
        float value=(float)bossData.Health/bossData.TempHealth;
        bossProgressBar.DOFillAmount(value,0.2f);
    }

    


    
}
