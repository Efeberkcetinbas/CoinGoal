using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DoorButtonTrigger : Obstacleable
{
    public int id;

    private MeshRenderer meshRenderer;

    [SerializeField] private Material greenMat,redMat;

    [SerializeField] private bool isUp=false;

    private void Start() 
    {
        meshRenderer=GetComponent<MeshRenderer>();
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
    }

    internal override void StopAction(Player player)
    {
        if(isUp)
        {
            EventManager.BroadcastId(GameEvent.OnCloseButton,id);
            meshRenderer.material=redMat;
        }
    }
}
