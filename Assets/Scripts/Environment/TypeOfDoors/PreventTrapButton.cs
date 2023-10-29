using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventTrapButton : DoorButtonControl
{
    //Ball buttona basiliyken trap kapanir. Diger top karsiya gecince o da oradaki butona basar. Bu sayede geride kalan top da karsiya gecebilir.
    [SerializeField] private int ID;

    //Prototype Pattern kullanilabilir mi farkli turlerde. Elektrik ates vb gibi
    internal override void OnOpenButton(int id)
    {
        if(id==this.ID)
        {
            EventManager.Broadcast(GameEvent.OnCloseTraps);
        }
    }

    internal override void OnCloseButton(int id)
    {
        if(id==this.ID)
        {
            EventManager.Broadcast(GameEvent.OnOpenTraps);
        }
    }

   
}
