using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Barrel : Obstacleable
{
    [SerializeField] private GameObject explosionEffect;

    [SerializeField] private BallData ballData;

    public Barrel()
    {
        canStay=false;
    }

    internal override void DoAction(Player player)
    {
        if(ballData.BallSpeed>5)
        {
            ballData.hitBarrel++;
            EventManager.Broadcast(GameEvent.OnBarrel);
            Instantiate(explosionEffect,transform.position,Quaternion.identity);
            //Restart olursa o leveldekiler level property kisminda acilir. O yuzden destroy yerine setActive            
            gameObject.SetActive(false);
        }

        
        if(ballData.hitBarrel>=3)
            EventManager.Broadcast(GameEvent.OnTrapHitPlayer);
    }
}
