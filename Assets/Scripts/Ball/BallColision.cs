using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColision : Obstacleable
{
    public BallColision()
    {
        interactionTag="Player";
    }

    internal override void DoAction(Player player)
    {
        Debug.Log("1 AZALDI");
    }
}
