using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Barrel : Obstacleable
{
    [SerializeField] private GameObject explosionEffect;

    [SerializeField] private BallData ballData;
    [SerializeField] private GameData gameData;

    public Barrel()
    {
        canStay=false;
    }

    internal override void DoAction(Player player)
    {
        if(!gameData.isGameEnd)
        {
            if(ballData.BallSpeed>10)
            {
                EventManager.Broadcast(GameEvent.OnBarrel);
                Instantiate(explosionEffect,transform.position,Quaternion.identity);
                //Restart olursa o leveldekiler level property kisminda acilir. O yuzden destroy yerine setActive            
                gameObject.SetActive(false);
                EventManager.Broadcast(GameEvent.OnTrapHitPlayer);
            }
        }
    }
}
