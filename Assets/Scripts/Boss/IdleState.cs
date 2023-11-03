using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    public MoveState moveState;

    public bool playerAttack;
    public override State RunCurrentState()
    {
        if(playerAttack)
            return moveState;

        else
            return this;
    }
}
