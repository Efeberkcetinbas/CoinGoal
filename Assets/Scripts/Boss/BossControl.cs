using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Unity.Mathematics;

public class BossControl : MonoBehaviour
{
    public BossData bossData;

    [SerializeField] private GameObject destroyParticle,Boss;
    
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
        Boss.transform.DOScale(Vector3.one*3,2f).OnComplete(()=>{
            Instantiate(destroyParticle,transform.position,quaternion.identity);
            StartCoroutine(CallEvents());
            gameObject.SetActive(false);
            //gameObject.SetActive(false);
            
        });
    }

    private IEnumerator CallEvents()
    {
        yield return new WaitForSeconds(2);
        EventManager.Broadcast(GameEvent.OnNormalBalls);
        EventManager.Broadcast(GameEvent.OnLoadNextLevel);
    }
  
}
