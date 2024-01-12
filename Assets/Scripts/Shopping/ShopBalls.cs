using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBalls : MonoBehaviour
{
    public int price;
    public int dataIndex;

    public bool isPurchased=false;
    public bool canBuy=false;


    public GameObject lockImage,goldImage;

    internal Button button;

    public TextMeshProUGUI priceText;

    
    public ShopBallData shopCharacterData;
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
        if(shopCharacterData.isPurchased)
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

        if(gameData.score>=price || shopCharacterData.isPurchased)
        {
            button.interactable=true;
            canBuy=true;
        }

        if(!shopCharacterData.isPurchased)
        {
            if(gameData.score<price)
            {
                button.interactable=false;
                canBuy=false;
            }
        }
    }
    
}
