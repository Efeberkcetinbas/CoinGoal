using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip GameOverSound,PassBetweenSound,TouchStartSound,TouchEndSound,HitWallSound,ButtonSound,BorderDownSound,GameStartSound,PlayerDeadSound,BounceSound,PortalSound,OnUpPortalSound,
    BridgeSound;

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
        EventManager.AddIdHandler(GameEvent.OnBordersDown,OnBordersDown);
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.AddHandler(GameEvent.OnPlayerDead,OnPlayerDead);
        EventManager.AddHandler(GameEvent.OnWindSound,OnGroundOpen);
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        EventManager.AddHandler(GameEvent.OnUpPortal,OnUpPortal);
        EventManager.AddHandler(GameEvent.OnBridgeOpen,OnBridgeOpen);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnTouchStart,OnTouchStart);
        EventManager.RemoveHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.RemoveIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.RemoveIdHandler(GameEvent.OnOpenButton,OnOpenButton);
        EventManager.RemoveIdHandler(GameEvent.OnBordersDown,OnBordersDown);
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnPlayerDead,OnPlayerDead);
        EventManager.RemoveHandler(GameEvent.OnWindSound,OnGroundOpen);
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        EventManager.RemoveHandler(GameEvent.OnUpPortal,OnUpPortal);
        EventManager.RemoveHandler(GameEvent.OnBridgeOpen,OnBridgeOpen);
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

    private void OnPlayerDead()
    {
        SetVolume(0.5f);
        effectSource.PlayOneShot(PlayerDeadSound);
    }

    private void OnGroundOpen()
    {
        SetVolume(0.4f);
        effectSource.PlayOneShot(BounceSound);
    }
    private void OnHitWall(int id)
    {
        SetVolume(0.2f);
        effectSource.PlayOneShot(HitWallSound);
    }

    private void OnOpenButton(int id)
    {
        SetVolume(0.3f);
        effectSource.PlayOneShot(ButtonSound);
    }

    private void OnBordersDown(int id)
    {
        SetVolume(0.1f);
        effectSource.PlayOneShot(BorderDownSound);
    }

    private void OnGameStart()
    {
        effectSource.PlayOneShot(GameStartSound);
    }

    private void OnPortalOpen()
    {
        effectSource.PlayOneShot(PortalSound);
    }

    private void OnUpPortal()
    {
        effectSource.PlayOneShot(OnUpPortalSound);
    }
    

    private void OnBridgeOpen()
    {
        effectSource.PlayOneShot(BridgeSound);
    }

    
    private float SetVolume(float vol)
    {
        return effectSource.volume=vol;
    }

  

}
