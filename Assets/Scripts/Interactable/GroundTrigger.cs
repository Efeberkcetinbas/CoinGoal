using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : Obstacleable
{
    public GroundTrigger()
    {
        canStay=false;
    }
    internal override void DoAction(Player player)
    {
        EventManager.Broadcast(GameEvent.OnBallsDivided);
        Destroy(gameObject);
    }
}
