using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    public Transform PortalPosition;

    private WaitForSeconds waitForSeconds;

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(3);
        portal.SetActive(false);
        
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnMiniGameFinish,OnMiniGameFinish);
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnMiniGameFinish,OnMiniGameFinish);
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
    }


    private void OnMiniGameFinish()
    {
        //Hemen gitmiyoruz. Boss'un Patlamasini Gormemiz Lazim
        //Gol Olunca Burasi Olsun
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

        /*
        EventManager.Broadcast(GameEvent.OnNormalBalls);
        EventManager.Broadcast(GameEvent.OnLoadNextLevel);*/
    }

    
}
