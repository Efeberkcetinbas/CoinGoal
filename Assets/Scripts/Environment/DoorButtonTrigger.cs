using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorButtonTrigger : Obstacleable
{
    public int id;

    private MeshRenderer meshRenderer;

    [SerializeField] private Material greenMat,redMat;

    [SerializeField] private bool isUp=false;
    [SerializeField] private ParticleSystem particle;

    private float oldScale;

    private void Start() 
    {
        meshRenderer=GetComponent<MeshRenderer>();
        oldScale=transform.localScale.y;
    }

    public DoorButtonTrigger()
    {
        canDamageToPlayer=false;
        canStay=false;
    }

    internal override void DoAction(Player player)
    {
        EventManager.BroadcastId(GameEvent.OnOpenButton,id);
        meshRenderer.material=greenMat;
        particle.Play();
        transform.DOScaleY(oldScale/1.5f,0.5f);
    }

    internal override void StopAction(Player player)
    {
        if(isUp)
        {
            EventManager.BroadcastId(GameEvent.OnCloseButton,id);
            meshRenderer.material=redMat;
            particle.Play();
            
        }

        transform.DOScaleY(oldScale,0.5f);
    }
}
