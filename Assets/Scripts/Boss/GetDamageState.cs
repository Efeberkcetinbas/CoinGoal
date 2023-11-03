using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamageState : State
{
    public int health=3;

    public DeathState deathState;
    public override State RunCurrentState()
    {
        if(health<=0)
            return deathState;
        else
            return this;
    }

    


}
