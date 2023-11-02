using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : Obstacleable
{
    [SerializeField] private GameObject destroyEffect;

    private bool isOne=false;
    internal override void DoAction(Player player)
    {
        if(player.ballData.isDestroyer)
        {
            Instantiate(destroyEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            if(!isOne)
            {
                EventManager.Broadcast(GameEvent.OnTrapHitPlayer);
                isOne=true;
            }
            
        }
        
    }
}
