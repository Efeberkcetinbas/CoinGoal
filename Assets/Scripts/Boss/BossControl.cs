using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BossControl : MonoBehaviour
{
    public BossData bossData;
    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossActive,OnBossActive);
        EventManager.AddHandler(GameEvent.OnBossDead,OnBossDead);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossActive,OnBossActive);
        EventManager.RemoveHandler(GameEvent.OnBossDead,OnBossDead);
    }

   


    private void OnBossActive()
    {

        //Cool bir sekilde sahneye gelir
    }

    private void OnBossDead()
    {
        //BURALAR DUZELTILECEK, DEGISTIRILECEK INVOKE KULLANILMAYACAK SAHNE DIZIMINDEN SONRA BAK
        //Destroy(gameObject);
        gameObject.SetActive(false);
        Invoke("NextLevel",2);
    }

    private void NextLevel()
    {
        EventManager.Broadcast(GameEvent.OnNormalBalls);
        EventManager.Broadcast(GameEvent.OnLoadNextLevel);
    }
}
