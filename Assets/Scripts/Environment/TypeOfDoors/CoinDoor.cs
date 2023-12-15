using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinDoor : DoorButtonControl
{
    [SerializeField] private int ID;

    [SerializeField] private float door1Y,door2Y,olddoor1Y,olddoor2Y,duration;

    [SerializeField] private Transform door1,door2;

    internal override void OnOpenButton(int id)
    {
        if(id==this.ID)
        {
            door1.DOLocalMoveY(door1Y,duration);
            door2.DOLocalMoveY(door2Y,duration);
        }
    }

    internal override void OnCloseButton(int id)
    {
        if(id==this.ID)
        {
            door1.DOLocalMoveY(olddoor1Y,duration);
            door2.DOLocalMoveY(olddoor2Y,duration);
        }
    }
}
