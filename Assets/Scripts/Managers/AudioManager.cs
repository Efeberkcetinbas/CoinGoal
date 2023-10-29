using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,PassBetweenSound,TouchStartSound,TouchEndSound;

    AudioSource musicSource,effectSource;

    private bool hit;

    private void Start() 
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        //musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.volume=0.4f;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.AddHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.AddHandler(GameEvent.OnTouchStart,OnTouchStart);
        EventManager.AddHandler(GameEvent.OnTouchEnd,OnTouchEnd);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnTouchStart,OnTouchStart);
        EventManager.RemoveHandler(GameEvent.OnTouchEnd,OnTouchEnd);
    }


    private void OnPassBetween()
    {
        effectSource.PlayOneShot(PassBetweenSound);   
    }

    private void OnGameOver()
    {
        effectSource.PlayOneShot(GameOverSound);
    }

    private void OnTouchStart()
    {
        effectSource.PlayOneShot(TouchStartSound);
    }

    private void OnTouchEnd()
    {
        effectSource.PlayOneShot(TouchEndSound);
    }

}
