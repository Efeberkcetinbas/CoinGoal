using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    
    [SerializeField] private GameObject explodeParticle;
    
    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerTakeDamage,OnPlayerTakeDamage);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerTakeDamage,OnPlayerTakeDamage);
        
    }
    private void OnPlayerTakeDamage()
    {
        //Cesitlendirilebilir
        Instantiate(explodeParticle,transform.position,Quaternion.identity);
        
    }

    
}
