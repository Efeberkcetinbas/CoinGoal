using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : Obstacleable
{
    public GetDamageState getDamageState;
    public StateManager stateManager;

    

    internal override void DoAction(Player player)
    {
        stateManager.currentState=getDamageState;
        Debug.Log("IS IT WORK");
    }
}
