using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorButtonTrigger : Obstacleable
{
    public int id;

    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private Material greenMat,redMat;

    [SerializeField] private bool isUp=false;
    [SerializeField] private ParticleSystem particle;

    private float oldScale,yaxis;

    private int enterId;

    private bool isInAnyBall=false;

     

    private void Start() 
    {
        oldScale=transform.localScale.y;
    }

    public DoorButtonTrigger()
    {
        canDamageToPlayer=false;
        canStay=false;
    }

    internal override void DoAction(Player player)
    {
        if(!isInAnyBall && !player.isInTheButton)
        {
            isInAnyBall=true;
            enterId=player.ID;
            EventManager.BroadcastId(GameEvent.OnOpenButton,id);
            meshRenderer.material=greenMat;
            particle.Play();
            transform.DOScaleY(oldScale/1.5f,0.5f);
            player.isInTheButton=true;
        }
        
    }

    internal override void StopAction(Player player)
    {
        if(enterId==player.ID)
        {
            if(isUp)
            {
                EventManager.BroadcastId(GameEvent.OnCloseButton,id);
                meshRenderer.material=redMat;
                particle.Play();
                isInAnyBall=false;
                player.isInTheButton=false; 
                transform.DOScaleY(oldScale,0.5f);
            }
            else
            {
                transform.DOLocalMoveY(-1.5f,1f).OnComplete(()=>gameObject.SetActive(false));
            }
                
        }
        
    }
}
