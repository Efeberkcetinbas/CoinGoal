using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public AttackState attackState;

    public bool canAttack;
    public override State RunCurrentState()
    {
        if(canAttack)
            return attackState;
        else
            return this;
    }
}
