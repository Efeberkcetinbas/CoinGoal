using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Obstacleable : MonoBehaviour
{
    float st = 0;
    internal float interval = 3;
    internal bool canStay=true;
    internal bool canInteract = true;
    internal bool canDamageToPlayer=true;
    internal string interactionTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            StartInteractWithEnemy(other.GetComponent<Player>());
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            InteractWithEnemy(other.GetComponent<Player>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == interactionTag)
        {
            StopInteractWithEnemy(other.GetComponent<Player>());
        }
    }

    void StartInteractWithEnemy(Player player)
    {
        DoAction(player);
    }

    void StopInteractWithEnemy(Player player)
    {
        StopAction(player);
    }

    void InteractWithEnemy(Player player)
    {
        st += Time.deltaTime;
        if (st > interval && canStay)
        {
            ResetProgress();
            DoAction(player);
        }
    }
    internal virtual void ResetProgress()
    {
        st = 0;
    }
    
    internal virtual void DoAction(Player player)
    {
        throw new System.NotImplementedException();
    }

    internal virtual void StopAction(Player player)
    {
        st = 0;
    }
    internal void StopInteract()
    {
        canInteract = false;
    }
    internal void StartInteract()
    {
        canInteract = true;
    }
}
