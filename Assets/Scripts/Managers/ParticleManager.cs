using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> pointParticles=new List<ParticleSystem>();

    [SerializeField] private List<ParticleSystem> successExplosion=new List<ParticleSystem>();


    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnSuccess);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnSuccess);
        
    }


    private void PlayPointParticles()
    {
        for (int i = 0; i < pointParticles.Count; i++)
        {
            pointParticles[i].Play();
        }
    }

    private void OnSuccess()
    {
        for (int i = 0; i < successExplosion.Count; i++)
        {
            successExplosion[i].Play();
        }
    }

    
}
