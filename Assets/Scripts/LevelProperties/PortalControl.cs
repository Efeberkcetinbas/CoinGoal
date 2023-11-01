using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    public Vector3 PortalPosition;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossDead,OnBossDead);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDead,OnBossDead);
    }


    private void OnBossDead()
    {
        portal.SetActive(true);
        EventManager.Broadcast(GameEvent.OnPortalOpen);
    }

    
}
