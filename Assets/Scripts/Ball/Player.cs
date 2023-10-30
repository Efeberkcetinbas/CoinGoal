using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    
    [SerializeField] private GameObject explodeParticle;
    public BallData ballData;
    
    public int ID;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerTakeDamage,OnPlayerTakeDamage);
        EventManager.AddIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.AddHandler(GameEvent.OnInvulnerable,OnInvulnerable);
        EventManager.AddHandler(GameEvent.OnVulnerable,OnVulnerable);
        EventManager.AddHandler(GameEvent.OnDestroyer,OnDestroyer);
        EventManager.AddHandler(GameEvent.OnNormal,OnNormal);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerTakeDamage,OnPlayerTakeDamage);
        EventManager.RemoveIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.RemoveHandler(GameEvent.OnInvulnerable,OnInvulnerable);
        EventManager.RemoveHandler(GameEvent.OnVulnerable,OnVulnerable);
        EventManager.RemoveHandler(GameEvent.OnDestroyer,OnDestroyer);
        EventManager.RemoveHandler(GameEvent.OnNormal,OnNormal);
        
    }
    private void OnPlayerTakeDamage()
    {
        //Cesitlendirilebilir
        Instantiate(explodeParticle,transform.position,Quaternion.identity);
        
    }

    private void OnHitWall(int id)
    {
        if(ID==id)
        {
            transform.DOScale(Vector3.one/1.3f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
        }
            
    }

    #region Buff
    private void OnInvulnerable()
    {
        Debug.Log("IAM INVINCIBLE");
        ballData.isInvulnerable=true;
    }
    private void OnVulnerable()
    {
        Debug.Log("OH NO ITS RED SUN");
        ballData.isInvulnerable=false;
    }

    private void OnDestroyer()
    {
        Debug.Log("SUPERMAN");
    }
    private void OnNormal()
    {
        Debug.Log("Clark Kent");
    }


    #endregion

    
}
