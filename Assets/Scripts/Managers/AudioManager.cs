using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,PassBetweenSound,TouchStartSound,TouchEndSound,HitWallSound,ButtonSound;

    AudioSource musicSource,effectSource;


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
        SetVolume(0.4f);
        effectSource.PlayOneShot(PassBetweenSound);   
    }

    private void OnGameOver()
    {
        effectSource.PlayOneShot(GameOverSound);
    }

    private void OnTouchStart()
    {
        SetVolume(0.1f);
        effectSource.PlayOneShot(TouchStartSound);
    }

    private void OnTouchEnd()
    {
        SetVolume(0.4f);
        effectSource.PlayOneShot(TouchEndSound);
    }

    private void OnHitWall(int id)
    {
        //Cok dikkat dagitiyor
        /*SetVolume(0.1f);
        effectSource.PlayOneShot(HitWallSound);*/
    }

    private void OnOpenButton(int id)
    {
        SetVolume(0.3f);
        effectSource.PlayOneShot(ButtonSound);
    }

    private float SetVolume(float vol)
    {
        return effectSource.volume=vol;
    }

  

}
