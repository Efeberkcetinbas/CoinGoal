using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinDoor : DoorButtonControl
{
    [SerializeField] private int ID;

    [SerializeField] private float door1X,door2X,olddoor1X,olddoor2X,duration;

    [SerializeField] private Transform door1,door2;

    internal override void OnOpenButton(int id)
    {
        if(id==this.ID)
        {
            door1.DOLocalMoveX(door1X,duration);
            door2.DOLocalMoveX(door2X,duration);
        }
    }

    internal override void OnCloseButton(int id)
    {
        if(id==this.ID)
        {
            door1.DOLocalMoveX(olddoor1X,duration);
            door2.DOLocalMoveX(olddoor2X,duration);
        }
    }
}
