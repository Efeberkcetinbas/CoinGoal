using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBalls : MonoBehaviour
{
    public int price;

    public bool isPurchased=false;
    public bool canBuy=false;

    public Image ballImage;

    public GameObject lockImage,goldImage,tickImage;

    internal Button button;

    public TextMeshProUGUI priceText;

    public Color color;

    public ShopBallData shopBallData;
    public GameData gameData;
    private void Start() 
    {
        button=GetComponent<Button>();
        priceText.text=price.ToString();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnButtonClicked,OnButtonClicked);
        EventManager.AddHandler(GameEvent.OnBallSelected,OnBallSelected);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnButtonClicked,OnButtonClicked);
        EventManager.RemoveHandler(GameEvent.OnBallSelected,OnBallSelected);
    }


    private void OnButtonClicked()
    {
        CheckPurchase();
    }

    private void OnBallSelected()
    {
        CheckPurchase();
    }

    private void CheckPurchase()
    {
        if(shopBallData.isPurchased)
        {
            //priceText.text="B";
            
            lockImage.SetActive(false);
            button.interactable=true;
            

            //button.image.color=Color.green;
            goldImage.SetActive(false);
            tickImage.SetActive(true);
            priceText.gameObject.SetActive(false);
            isPurchased=true;
        }

        if(gameData.coin>=price || shopBallData.isPurchased)
        {
            button.interactable=true;
            canBuy=true;
        }

        else
        {
            button.interactable=false;
            canBuy=false;
        }
    }
}
