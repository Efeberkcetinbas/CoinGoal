using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Sticky : Obstacleable
{
    [SerializeField] private Ease ease;

    internal override void DoAction(Player player)
    {
        player.SetZeroBallsRigidbody();
        EventManager.Broadcast(GameEvent.OnSticky);
        transform.DOScale(Vector3.one*1.25f,0.25f).OnComplete(()=>transform.DOScale(Vector3.one*1,0.25f)).SetEase(ease);
    }

}
