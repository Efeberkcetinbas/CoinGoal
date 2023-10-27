using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinDoor : DoorButtonControl
{
    //Coinlerle dolu oda. İceri girdigimizde kasayi acariz. Kapi kapanir. Diger top ile kapiyi tekrar acariz.
    [SerializeField] private int ID;

    [SerializeField] private float y,old_y,duration;

    internal override void OnOpenButton(int id)
    {
        if(id==this.ID)
        {
            transform.DOLocalMoveY(y,duration);
        }
    }

    internal override void OnCloseButton(int id)
    {
        if(id==this.ID)
        {
            transform.DOLocalMoveY(old_y,duration);
        }
    }
}
