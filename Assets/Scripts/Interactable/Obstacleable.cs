using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Obstacleable : MonoBehaviour
{
    float st = 0;
    internal float interval = 3;
    internal bool canInteract = true;
    internal string interactionTag = "Player";



    void OnTriggerExit(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            StartInteractWithPlayer(other.GetComponent<Player>());
        }
    }
    
    // !!!!!!!!!!!!!!
    //Kaldigi sure boyunca burasi da aktif oluyor
    void OnTriggerStay(Collider other)
    {
        /*if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            InteractWithPlayer(other.GetComponent<Player>());
        }*/
    }
    /*void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == interactionTag)
        {
            InteractionExit(other.GetComponent<Player>());
        }
    }
*/
    void StartInteractWithPlayer(Player player)
    {
        DoAction(player);
    }

    void InteractWithPlayer(Player player)
    {
        st += Time.deltaTime;
        if (st > interval)
        {
            ResetProgress();
            DoAction(player);
        }
    }
    //virtuallarin hepsini implemente ediyor
    internal virtual void ResetProgress()
    {
        st = 0;
    }
    internal virtual void InteractionExit(Player player)
    {
        throw new System.NotImplementedException();
    }
    internal virtual void DoAction(Player player)
    {
        throw new System.NotImplementedException();
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
