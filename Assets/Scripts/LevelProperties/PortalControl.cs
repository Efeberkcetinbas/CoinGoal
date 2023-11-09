using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    public Vector3 PortalPosition;

    private WaitForSeconds waitForSeconds;

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(3);
        
    }

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
        StartCoroutine(OpenIt());
    }

    

    private void OnPortalOpen()
    {
        portal.SetActive(true);
    }

    private IEnumerator OpenIt()
    {
        yield return waitForSeconds;
        portal.SetActive(true);
        EventManager.Broadcast(GameEvent.OnPortalOpen);
    }

    
}
