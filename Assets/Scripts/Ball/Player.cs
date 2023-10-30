using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    
    [SerializeField] private GameObject explodeParticle;
    
    public int ID;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerTakeDamage,OnPlayerTakeDamage);
        EventManager.AddIdHandler(GameEvent.OnHitWall,OnHitWall);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerTakeDamage,OnPlayerTakeDamage);
        EventManager.RemoveIdHandler(GameEvent.OnHitWall,OnHitWall);
        
    }
    private void OnPlayerTakeDamage()
    {
        //Cesitlendirilebilir
        Instantiate(explodeParticle,transform.position,Quaternion.identity);
        
    }

    private void OnHitWall(int id)
    {
        if(ID==id)
            Debug.Log("WORK TIME");
    }

    
}
