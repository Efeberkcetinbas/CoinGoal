using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSpecification : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticle;
    
    [SerializeField] private int ID;

    public GameData gameData;

    private void OnEnable() 
    {
        EventManager.AddIdHandler(GameEvent.OnBordersDown,OnBordersDown);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveIdHandler(GameEvent.OnBordersDown,OnBordersDown);
        
    }

    private void OnBordersDown(int id)
    {
        id=ID;
        if(gameData.BorderIndex==id)
        {
            dustParticle.Play();
        }
        
    }
}
