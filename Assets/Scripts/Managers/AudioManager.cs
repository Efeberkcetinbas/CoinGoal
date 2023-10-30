using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,PassBetweenSound,TouchStartSound,TouchEndSound,HitWallSound,ButtonSound;

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
        EventManager.AddIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.AddIdHandler(GameEvent.OnOpenButton,OnOpenButton);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnTouchStart,OnTouchStart);
        EventManager.RemoveHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.RemoveIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.RemoveIdHandler(GameEvent.OnOpenButton,OnOpenButton);
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

    private void OnHitWall(int id)
    {
        effectSource.PlayOneShot(HitWallSound);
    }

    private void OnOpenButton(int id)
    {
        effectSource.PlayOneShot(ButtonSound);
    }

  

}
