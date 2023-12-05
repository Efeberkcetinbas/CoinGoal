using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EdgeControl : Obstacleable
{
    public EdgeControl()
    {
        canStay=false;
    }

    internal override void DoAction(Player player)
    {
        if(player.ballData.BallSpeed>10)
            transform.DOScale(transform.localScale*1.1f,0.1f).OnComplete(()=>transform.DOScale(Vector3.one,0.1f))        ;
    }
}
