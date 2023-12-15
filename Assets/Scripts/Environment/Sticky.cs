using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sticky : Obstacleable
{
    internal override void DoAction(Player player)
    {
        player.SetZeroBallsRigidbody();
        EventManager.Broadcast(GameEvent.OnSticky);
    }

}
