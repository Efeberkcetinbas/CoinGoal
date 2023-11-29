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

    //Bir Canvas‘ı gizlemek için SetActive(false) yerine enabled=false‘u tercih edin
    public GameObject failPanel;
    [SerializeField] private Ease ease;


    [Header("Open/Close")]
    [SerializeField] private GameObject[] open_close;
    [SerializeField] private GameObject[] NormalBalls;
    [SerializeField] private GameObject[] BossBalls;
    //Boss Ball

    private WaitForSeconds waitForSeconds;


    private void Awake() 
    {
        ClearData();
        //StarterPack();
    }

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(2);
        StarterPack();
    }
    
    private void StarterPack()
    {
        if(gameData.LevelNumberIndex%5!=0)
        {
            OnNormalBalls();
            Debug.Log("BURAYA MI GIRIR USTA");
        }

        else
        {

            Debug.Log("IS WORK");
            OnBossBall();
            EventManager.Broadcast(GameEvent.OnBossActive);
            EventManager.Broadcast(GameEvent.OnBossBall);
        }
    }
    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPassBetween, OnPassBetween);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.AddHandler(GameEvent.OnDamagePlayer,OnDamagePlayer);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.AddHandler(GameEvent.OnPlayerDead,OnPlayerDead);
        EventManager.AddHandler(GameEvent.OnNormalBalls,OnNormalBalls);
        EventManager.AddHandler(GameEvent.OnBossBall,OnBossBall);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPassBetween, OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnDamagePlayer,OnDamagePlayer);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnPlayerDead,OnPlayerDead);
        EventManager.RemoveHandler(GameEvent.OnNormalBalls,OnNormalBalls);
        EventManager.RemoveHandler(GameEvent.OnBossBall,OnBossBall);
    }

    //Boss Ball and Normal Ball Activity Settings.
    //Boss Ball Canvas Open and the others false
    private void OnBossBall()
    {
        OpenClose(BossBalls,true);
        OpenClose(NormalBalls,false);
        gameData.isBossLevel=true;
    }

    private void OnNormalBalls()
    {
        OpenClose(BossBalls,false);
        OpenClose(NormalBalls,true);
        gameData.isBossLevel=false;
    }
    

    private void OnRestartLevel()
    {
        OnNextLevel();
        failPanel.transform.localScale=Vector3.zero;
        failPanel.SetActive(false);
    }

    private void OnPlayerDead()
    {
        StartCoroutine(OpenFailPanel());
    }

    private IEnumerator OpenFailPanel()
    {
        yield return waitForSeconds;
        OnGameOver();
    }
    //Her 5 Levelde 1
   

    private void OnDamagePlayer()
    {
        gameData.isGameEnd=true;
    }

    #region LEVEL PROPERTIES

    //When Level Change Update Req Ball Pass Number
    private void UpdateRequirement()
    {
        if(gameData.LevelNumberIndex%5!=0)
        {
            gameData.LevelRequirementNumber=FindObjectOfType<RequirementControl>().RequirementNumber;
            EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
        }
        //Update UI
    }

    private void OnPassBetween()
    {
        EventManager.Broadcast(GameEvent.OnIncreaseScore);
        float value=1/(float)gameData.LevelRequirementNumber;
        gameData.ProgressNumber+=value;
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
        gameData.levelProgressNumber++;

        if(gameData.LevelRequirementNumber==gameData.levelProgressNumber)
            StartCoroutine(LevelFinish());
        //Update UI progress bar
    }

    private IEnumerator LevelFinish()
    {
        gameData.isGameEnd=true;
        Debug.Log("Duration:");
        yield return waitForSeconds;
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //BOLUM SONU ATIS OYUNU
        //SAHNEDE PUANLI HEDEFLER BELIRIYOR VE ONLARA SIRASIYLA UC ATIS YAPIYORUZ. DAHA SONRA PORTAL YERINE SUCCESS OLUYOR
        /*Debug.Log("WAIT Duration:"); */
        EventManager.Broadcast(GameEvent.OnPortalOpen);

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
        ClearData();
        EventManager.Broadcast(GameEvent.OnUIRequirementUpdate);
        StarterPack();
    }

    private void OnGameStart()
    {
        UpdateRequirement();
    }

    
    
    void ClearData()
    {
        ballData.isItPassed=false;
        gameData.isGameEnd=true;
        gameData.canChangeIndex=true;
        gameData.ProgressNumber=0;
        gameData.levelProgressNumber=0;
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
