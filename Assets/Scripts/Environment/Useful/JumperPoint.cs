using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumperPoint : Obstacleable
{
    [SerializeField] private float jumpForce;

    [SerializeField] private Vector3 powerType;
    internal override void DoAction(Player player)
    {
       /* player.SetTempRigidbody();

        //Sadece aktif top icin
        if(player.isOrderMe)
        {
            player.tempRigidbody.AddForce(powerType*jumpForce,ForceMode.Impulse);
            EventManager.Broadcast(GameEvent.OnWindSound);
        }*/

        player.ballsRigidbody.AddForce(powerType*jumpForce,ForceMode.Impulse);
        EventManager.Broadcast(GameEvent.OnWindSound);
        
    }
}
