using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : Obstacleable
{
    private int ID;
    internal override void DoAction(Player player)
    {
        ID=player.ID;
        EventManager.BroadcastId(GameEvent.OnHitWall,ID);
    }
}
