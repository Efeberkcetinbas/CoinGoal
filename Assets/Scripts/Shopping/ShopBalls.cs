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
        CheckPurchase();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnShopBallSelected,OnBallSelected);
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopOpen);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnShopBallSelected,OnBallSelected);
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopOpen);
    }


    

    private void OnBallSelected()
    {
        CheckPurchase();
    }

    private void OnShopOpen()
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
            //tickImage.SetActive(true);
            priceText.gameObject.SetActive(false);
            isPurchased=true;

        }

        if(gameData.score>=price || shopBallData.isPurchased)
        {
            button.interactable=true;
            canBuy=true;
        }

        if(!shopBallData.isPurchased)
        {
            if(gameData.score<price)
            {
                button.interactable=false;
                canBuy=false;
            }
        }
    }

    
}
