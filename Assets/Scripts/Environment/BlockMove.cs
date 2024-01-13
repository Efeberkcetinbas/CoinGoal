using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockMove : MonoBehaviour
{
    [SerializeField] private bool isUpDown,isLeftRight,isJumper;

    [SerializeField] private float x,oldx,y,oldy,z,oldz,duration;
    [SerializeField] private Ease ease;

    [SerializeField] private Transform block;

    private bool isWorking=false;

    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameStart,Move);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart,Move);
        
    }

    private void Move()
    {
        if(!isWorking)
        {
            if(isLeftRight) block.DOLocalMoveX(x,duration).OnComplete(()=>block.DOLocalMoveX(oldx,duration)).SetLoops(-1,LoopType.Yoyo).SetEase(ease);
            if(isJumper) block.DOLocalMoveY(y,duration).OnComplete(()=>block.DOLocalMoveY(oldy,duration)).SetLoops(-1,LoopType.Yoyo).SetEase(ease);
            if(isUpDown) block.DOLocalMoveZ(z,duration).OnComplete(()=>block.DOLocalMoveZ(oldz,duration)).SetLoops(-1,LoopType.Yoyo).SetEase(ease);
            isWorking=true;
        }
    }
}
