using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumperPoint : Obstacleable
{
    [SerializeField] private float jumpForce;

    [SerializeField] private Vector3 powerType;

    [SerializeField] private GameData gameData;
    internal override void DoAction(Player player)
    {
       /* player.SetTempRigidbody();

        //Sadece aktif top icin
        if(player.isOrderMe)
        {
            player.tempRigidbody.AddForce(powerType*jumpForce,ForceMode.Impulse);
            EventManager.Broadcast(GameEvent.OnWindSound);
        }*/

        if(!gameData.isGameEnd)
        {
            player.ballsRigidbody.AddForce(powerType*jumpForce,ForceMode.Impulse);
            EventManager.Broadcast(GameEvent.OnWindSound);
        }

        
        
    }
}
