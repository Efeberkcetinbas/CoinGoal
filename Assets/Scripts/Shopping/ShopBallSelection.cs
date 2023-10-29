using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBallSelection : MonoBehaviour
{
    public List<ShopBalls> shopBalls=new List<ShopBalls>();
    
    public Color color;

    //Purchase
    public void SelectBall(int selectedIndex)
    {
        if(shopBalls[selectedIndex-1].button.interactable)
        {
            //Throw Event
            shopBalls[selectedIndex-1].lockImage.SetActive(false);
            if(!shopBalls[selectedIndex-1].isPurchased)
                //Decrease Money
                //-shopBalls[selectedIndex-1].price);
            
            shopBalls[selectedIndex-1].shopBallData.isPurchased=true;

            for (int i = 0; i < shopBalls.Count; i++)
            {
                shopBalls[i].button.image.color=Color.white;
            }

            shopBalls[selectedIndex-1].button.image.color=color;
        }
    }
}
