using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : Obstacleable
{
    [SerializeField] private GameObject destroyEffect;

    [SerializeField] private GameData gameData;

    private bool isOne=false;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        
    }


    internal override void DoAction(Player player)
    {
        if(!gameData.isGameEnd)
        {
            if(player.isOrderMe)
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
                        isOne=true;
                        EventManager.Broadcast(GameEvent.OnTrapHitPlayer);
                        Debug.Log("KAC KEZ CAGIRILDI");
                    }
                    
                }
            }
        }           
    }


    private void OnRestartLevel()
    {
        isOne=false;
    }
}
