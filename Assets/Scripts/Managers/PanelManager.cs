using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,BuffPanel,BallsPanel,ScoreImage,DiamondImage;


    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,BuffX,BuffY,BallX,BallY,duration, scoreX,oldScoreX,diamondX,oldDiamondX;

    public GameData gameData;

    public GameObject UICanvas;

    [SerializeField] private ParticleSystem skinParticle;


    //Insert Coin
    [SerializeField] private RectTransform coinImage;

    private WaitForSeconds waitForSeconds;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnMiniGameActive,OnMiniGameActive);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.AddHandler(GameEvent.OnBallMeshChange,OnBallMeshChange);
        EventManager.AddHandler(GameEvent.OnIncreaseScore,OnIncreaseScore);
        EventManager.AddHandler(GameEvent.OnIncreaseGold,OnIncreaseGold);
    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnMiniGameActive,OnMiniGameActive);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnBallMeshChange,OnBallMeshChange);
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore,OnIncreaseScore);
        EventManager.RemoveHandler(GameEvent.OnIncreaseGold,OnIncreaseGold);
    }

    private void Start() 
    {
        //UICanvas.SetActive(false);
        waitForSeconds=new WaitForSeconds(1);
    }

    
    
    
    public void StartGame() 
    {
        
        coinImage.DOAnchorPosY(0,1f).OnComplete(()=>coinImage.transform.DORotate(new Vector3(0,90,0), 1f).OnComplete(()=>{
            gameData.isGameEnd=false;
            StartPanel.gameObject.SetActive(false);
            ScoreImage.DOAnchorPosX(oldScoreX,1f);
            DiamondImage.DOAnchorPosX(oldDiamondX,1f);

            //UICanvas.SetActive(true);
            EventManager.Broadcast(GameEvent.OnGameStart);
        }));
    }

    private void OnMiniGameActive()
    {
        //UICanvas.SetActive(false);
    }

    private void OnRestartLevel()
    {
        OnNextLevel();
    }

    private void OnIncreaseScore()
    {
        ScoreImage.DOAnchorPosX(scoreX,.5f).OnComplete(()=>StartCoroutine(WaitForImage(ScoreImage,oldScoreX)));
    }

    private void OnIncreaseGold()
    {
        DiamondImage.DOAnchorPosX(diamondX,.5f).OnComplete(()=>StartCoroutine(WaitForImage(DiamondImage,oldDiamondX)));
    }

    private IEnumerator WaitForImage(RectTransform rectTransform,float val)
    {
        yield return waitForSeconds;
        rectTransform.DOAnchorPosX(val,1f);
        
    }

    private void OnNextLevel()
    {
        StartPanel.gameObject.SetActive(true);
        coinImage.DOAnchorPosY(-200,0.2f).OnComplete(()=>coinImage.transform.DORotate(new Vector3(0,0,0), 0.2f));
        ScoreImage.DOAnchorPosX(scoreX,.5f);
        StartPanel.DOAnchorPos(Vector2.zero,0.1f);
        StartCoroutine(Blink(Fade.gameObject,Fade));
    }


  

    private IEnumerator Blink(GameObject gameObject,Image image)
    {
        
        gameObject.SetActive(true);
        image.color=new Color(0,0,0,1);
        image.DOFade(0,2f);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

    }


    public void OpenBuffsPanel()
    {
        EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        BuffPanel.gameObject.SetActive(true);
        BuffPanel.DOAnchorPos(Vector2.zero,duration);
        EventManager.Broadcast(GameEvent.OnOpenBuffPanel);
    }

    public void OpenBallsPanel()
    {
        EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        BallsPanel.gameObject.SetActive(true);
        BallsPanel.DOAnchorPos(Vector2.zero,duration);
        EventManager.Broadcast(GameEvent.OnShopOpen);
    }

    public void BackToStart(bool isOnCharacter)
    {

        if(isOnCharacter)
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            BuffPanel.DOAnchorPos(new Vector2(BuffX,BuffY),duration);
            //.OnComplete(()=>CharacterPanel.gameObject.SetActive(false));
        }
        else
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            BallsPanel.DOAnchorPos(new Vector2(BallX,BallY),duration);
            //.OnComplete(()=>WeaponPanel.gameObject.SetActive(false));
        }

        EventManager.Broadcast(GameEvent.OnShopClose);

        EventManager.Broadcast(GameEvent.OnButtonClicked);

    }


    private void OnBallMeshChange()
    {
        skinParticle.Play();
    }

}
