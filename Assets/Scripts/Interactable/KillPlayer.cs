using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : Obstacleable
{
    internal override void DoAction(Player player)
    {
        EventManager.Broadcast(GameEvent.OnPlayerTakeDamage);
    }
}
