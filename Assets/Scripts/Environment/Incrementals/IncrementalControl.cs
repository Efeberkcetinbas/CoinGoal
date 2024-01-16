using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IncrementalControl : MonoBehaviour
{
    [SerializeField] private GameData gameData;    

    [Header("Incremental / Texts ")]
    [SerializeField] private TextMeshProUGUI scoreIncrementalText;
    [SerializeField] private TextMeshProUGUI diamondIncrementalText;
    [SerializeField] private TextMeshProUGUI scorePriceText;
    [SerializeField] private TextMeshProUGUI diamondPriceText;
    [SerializeField] private TextMeshProUGUI buffTimeText;
    [SerializeField] private TextMeshProUGUI buffTimePriceText;

    [Header("Incremental / Buttons")]
    [SerializeField] private Button scoreIncrementalButton;
    [SerializeField] private Button diamondIncrementalButton;
    [SerializeField] private Button buffTimeButton;


    private void Start()
    {
        OnOpenIncrementalPanel();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnOpenIncrementalPanel,OnOpenIncrementalPanel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnOpenIncrementalPanel,OnOpenIncrementalPanel);
    }
    private void OnOpenIncrementalPanel()
    {
        SetPriceTexts(scoreIncrementalText,gameData.increaseScore,scorePriceText,gameData.priceForIncreaseScore);
        SetPriceTexts(diamondIncrementalText,gameData.increaseCoinAmount,diamondPriceText,gameData.priceForIncreaseDiamond);
        SetPriceTexts(buffTimeText,gameData.BuffTime,buffTimePriceText,gameData.priceForBuffTime);

        CheckButtons(gameData.priceForIncreaseScore,gameData.score,scoreIncrementalButton);
        CheckButtons(gameData.priceForIncreaseDiamond,gameData.diamond,diamondIncrementalButton);
        CheckButtons(gameData.priceForBuffTime,gameData.score,buffTimeButton);
    }

    private void SetPriceTexts(TextMeshProUGUI textMeshProUGUI,int val,TextMeshProUGUI price,int priceVal)
    {
        textMeshProUGUI.SetText(val.ToString());
        price.SetText(priceVal.ToString());
    }

    private void CheckButtons(int val,int typeOfIncremental,Button button)
    {
        if(val<=typeOfIncremental)
            button.interactable=true;
        else
            button.interactable=false;
    }

    public void SetIncreaseScoreIncremental()
    {
        if(gameData.score>=gameData.priceForIncreaseScore)
        {
            gameData.score-=gameData.priceForIncreaseScore;
            gameData.increaseScore+=5;
            gameData.priceForIncreaseScore*=2;
            SetPriceTexts(scoreIncrementalText,gameData.increaseScore,scorePriceText,gameData.priceForIncreaseScore);
            CheckButtons(gameData.priceForIncreaseScore,gameData.score,scoreIncrementalButton);
        }
    }

    public void SetIncrementalDiamondIncremental()
    {
        //diamond yerine score ile de olabilir.
        if(gameData.diamond>=gameData.priceForIncreaseDiamond)
        {
           gameData.diamond-=gameData.priceForIncreaseDiamond;
           gameData.increaseCoinAmount+=5;
           gameData.priceForIncreaseDiamond*=2;
           SetPriceTexts(diamondIncrementalText,gameData.increaseCoinAmount,diamondPriceText,gameData.priceForIncreaseDiamond);
           CheckButtons(gameData.priceForIncreaseDiamond,gameData.diamond,diamondIncrementalButton);
        }
    }

    public void SetBuffTimeIncrease()
    {
        if(gameData.score>=gameData.priceForBuffTime)
        {
            gameData.score-=gameData.priceForBuffTime;
            gameData.BuffTime+=1;
            gameData.priceForBuffTime*=2;
            SetPriceTexts(buffTimeText,gameData.BuffTime,buffTimePriceText,gameData.priceForBuffTime);
            CheckButtons(gameData.priceForBuffTime,gameData.score,buffTimeButton);
        }
    }

    
}
