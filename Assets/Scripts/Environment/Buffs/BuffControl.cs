using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffControl : MonoBehaviour
{
    //Invincle, Destroyer

    
    public GameData gameData;
    public BallData ballData;

    [SerializeField] private bool isBuffused=false;

    [Header("Buff Values / Boolean")]
    [SerializeField] private bool isSpeedUp;
    [SerializeField] private bool isDestroyer;
    [SerializeField] private bool isInvulnerable;

    [Header("Buff Values / Buttons")]
    [SerializeField] private Button speedUpButton;
    [SerializeField] private Button destroyerButton;
    [SerializeField] private Button invulnerableButton;

    

    [Header("Buff Values / Texts")]
    [SerializeField] private TextMeshProUGUI speedUpText;
    [SerializeField] private TextMeshProUGUI destroyerText;
    [SerializeField] private TextMeshProUGUI invulnerableText;



    [SerializeField] private GameObject buffBar;

    private void Start()
    {
        OnOpenBuffPanel();
    }
    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnOpenBuffPanel,OnOpenBuffPanel);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnOpenBuffPanel,OnOpenBuffPanel);
    }

    private void OnNextLevel()
    {
        isBuffused=false;
    }

    private void OnOpenBuffPanel()
    {
        SetPriceTexts(speedUpText,ballData.PriceValSpeed);
        SetPriceTexts(destroyerText,ballData.PriceValDestroyer);
        SetPriceTexts(invulnerableText,ballData.PriceValInvulnerability);

        CheckButtons(ballData.PriceValSpeed,speedUpButton);
        CheckButtons(ballData.PriceValDestroyer,destroyerButton);
        CheckButtons(ballData.PriceValInvulnerability,invulnerableButton);
    }

    #region BuffUIControl
    private void SetPriceTexts(TextMeshProUGUI textMeshProUGUI,int val)
    {
        textMeshProUGUI.SetText(val.ToString());
    }

    private void CheckButtons(int val,Button button)
    {
        if(val<=gameData.diamond)
        {
            button.interactable=true;
        }
        else
        {

            button.interactable=false;
        }
    }

    private void BuyBuff(bool val,int priceVal,Button button,TextMeshProUGUI textMeshProUGUI)
    {
        if(gameData.diamond>=priceVal)
        {
            val=true;
            isBuffused=true;
            gameData.diamond-=priceVal;
            EventManager.Broadcast(GameEvent.OnBoughtBuff);
            ballData.SaveData();
        }

        else
        {
            return;
        }
    }

    #endregion

    #region Buffs Panel

    public void SetDestroyer()
    {
        BuyBuff(isDestroyer,ballData.PriceValDestroyer,destroyerButton,destroyerText);
        isDestroyer=true;
        ballData.PriceValDestroyer*=2;
        OnOpenBuffPanel();
    }
    public void SetSpeedUp()
    {
        BuyBuff(isSpeedUp,ballData.PriceValSpeed,speedUpButton,speedUpText);
        isSpeedUp=true;
        ballData.PriceValSpeed*=2;
        SetPriceTexts(speedUpText, ballData.PriceValSpeed);
        OnOpenBuffPanel();
    }
    public void SetInvulnerable()
    {
        BuyBuff(isInvulnerable,ballData.PriceValInvulnerability,invulnerableButton,invulnerableText);
        isInvulnerable=true;
        ballData.PriceValInvulnerability*=2;
        SetPriceTexts(invulnerableText, ballData.PriceValInvulnerability);
        OnOpenBuffPanel();
    }

    
    

    #endregion 

    
    #region Buffs
    private void DoInvincle()
    {
        isBuffused=true;
        EventManager.Broadcast(GameEvent.OnInvulnerable);
        StartCoroutine(DoReverse(GameEvent.OnVulnerable));

        //Ekonomi lazim. Buff aldigimizda ucret artmali. 
        //Buff'in baslamasini game is Start eventine bagla
    }

    private void DoDestroyer()
    {
        isBuffused=true;
        EventManager.Broadcast(GameEvent.OnDestroyer);
        StartCoroutine(DoReverse(GameEvent.OnNormal));
    }

    private void DoSpeedUp()
    {
        isBuffused=true;
        EventManager.Broadcast(GameEvent.OnSpeedUp);
        StartCoroutine(DoReverse(GameEvent.OnSpeedNormal));
    }

    #endregion

    private void OnGameStart()
    {
        if(isBuffused)
        {
            if(isDestroyer) DoDestroyer();
            if(isInvulnerable) DoInvincle();
            if(isSpeedUp) DoSpeedUp();
            
            buffBar.SetActive(true);
            EventManager.Broadcast(GameEvent.OnUpdateBuff);
        }
    }

    



    #region Caroutines
    //Sure baslangicini iyi ayarlamak lazim
    private IEnumerator DoReverse(GameEvent gameEvent)
    {
        yield return new WaitForSeconds(gameData.BackTime);
        EventManager.Broadcast(gameEvent);
    }
    #endregion
    
}
