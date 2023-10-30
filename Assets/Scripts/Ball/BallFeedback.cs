using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFeedback : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> TextParticles=new List<ParticleSystem>();

    private int index;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPassBetween,OnPassBetween);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
    }

    private void SetRandomText()
    {
        index=Random.Range(0,TextParticles.Count);
    }


    private void PlayRandomText()
    {
        SetRandomText();
        TextParticles[index].Play();
    }

    private void OnPassBetween()
    {
        PlayRandomText();
    }
}
