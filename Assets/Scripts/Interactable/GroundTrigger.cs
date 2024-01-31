using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : Obstacleable
{
    public GroundTrigger()
    {
        canStay=false;
    }

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
        EventManager.Broadcast(GameEvent.OnBallsDivided);
        gameObject.SetActive(false);
    }

    private void OnRestartLevel()
    {
        gameObject.SetActive(true);
    }
}
