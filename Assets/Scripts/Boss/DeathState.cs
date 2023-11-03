using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("IM DEAD");
        return this;
    }

    
}
