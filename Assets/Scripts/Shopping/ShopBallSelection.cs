using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopBallSelection : MonoBehaviour
{
    public List<ShopBalls> shopBalls=new List<ShopBalls>();
    
    public Color color;
    public GameData gameData;
    public BallData ballData;

    private void Start() 
    {
        //shopBalls[ballData.selectedBallIndex].button.image.color=Color.green;
    }

    //Purchase
    public void SelectBall(int selectedIndex)
    {
        if(shopBalls[selectedIndex].button.interactable)
        {
            //Throw Event
            shopBalls[selectedIndex].lockImage.SetActive(false);
            if(!shopBalls[selectedIndex].isPurchased)
            {
                gameData.score-=shopBalls[selectedIndex].price;
                shopBalls[selectedIndex].shopBallData.isPurchased=true;
                EventManager.Broadcast(GameEvent.OnShopBallSelected);
                EventManager.Broadcast(GameEvent.OnUIUpdate);
            }
            
            

            for (int i = 0; i < shopBalls.Count; i++)
            {
                shopBalls[i].button.image.color=Color.white;
                shopBalls[i].transform.DOScale(Vector3.one,0.2f);
            }

            shopBalls[selectedIndex].button.image.color=Color.green;
            shopBalls[selectedIndex].transform.DOScale(Vector3.one*1.2f,0.2f);
            ballData.selectedBallIndex=selectedIndex;
            EventManager.Broadcast(GameEvent.OnBallMeshChange);
        }
    }
}
