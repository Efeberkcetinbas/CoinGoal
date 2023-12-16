using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinDoor : DoorButtonControl
{
    [SerializeField] private int ID;

    [SerializeField] private float doorY,olddoorY,duration;

    [SerializeField] private Transform door;

    internal override void OnOpenButton(int id)
    {
        if(id==this.ID)
        {
            door.DOLocalMoveY(doorY,duration);
        }
    }

    internal override void OnCloseButton(int id)
    {
        if(id==this.ID)
        {
            door.DOLocalMoveY(olddoorY,duration);
        }
    }
}
