using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : Obstacleable
{
    [SerializeField] private GameObject destroyEffect;
    internal override void DoAction(Player player)
    {
        if(player.ballData.isDestroyer)
        {
            Instantiate(destroyEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            EventManager.Broadcast(GameEvent.OnTrapHitPlayer);
        }
        
    }
}
