using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    //Belirli sure mermim kadar ates ediyorum. 
    //Object Pooling Kullanimi

    public bool isBulletEmpty=true;
    public IdleState IdleState;
    
    public override State RunCurrentState()
    {
        if(!isBulletEmpty)
        {
            Debug.Log("Kucuk Testereler Gonderiyorum belirli sayida ve aralikta");
            //Mermim bitince nefesleniyorum
            return IdleState;
        }
            

        else
            return this;
    }
}
