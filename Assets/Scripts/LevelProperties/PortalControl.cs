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
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDead,OnBossDead);
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }


    private void OnBossDead()
    {
        //Hemen gitmiyoruz. Boss'un Patlamasini Gormemiz Lazim
        portal.SetActive(true);
        EventManager.Broadcast(GameEvent.OnPortalOpen);
    }

    

    private void OnPortalOpen()
    {
        portal.SetActive(true);
    }

    
}
