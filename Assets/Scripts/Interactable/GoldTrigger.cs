using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class GoldTrigger : Obstacleable
{
    [SerializeField] private GameObject coinEffectPrefab;
    [SerializeField] private Transform pointPos;
    [SerializeField] private GameObject coinGameObject;

    [SerializeField] private GameData gameData;

    //<T>    
    private SphereCollider sphereCollider;

    [SerializeField] private bool isDiamond;

    private void Start() 
    {
        sphereCollider=GetComponent<SphereCollider>();
    }
    
    internal override void DoAction(Player player)
    {
        if(!isDiamond)
            EventManager.Broadcast(GameEvent.OnIncreaseScore);
        else
            EventManager.Broadcast(GameEvent.OnIncreaseGold);
            
        StartCoinMove();
    }   

    internal override void StopAction(Player player)
    {
        base.StopAction(player);
    }


    private void StartCoinMove()
    {
        sphereCollider.enabled=false;
        coinGameObject.SetActive(false);

        GameObject coin=Instantiate(coinEffectPrefab,pointPos.transform.position,coinEffectPrefab.transform.rotation);
        coin.transform.DOLocalJump(coin.transform.localPosition,1,1,1,false);
        //coin.transform.DOScale(Vector3.zero,1.5f);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + gameData.increaseCoinAmount.ToString();
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        //Daha duzgun bir sonuc bul. Pooling ile sahnede olusturup her levelde level generator yazip coin konumlari atayip ayni coinleri kullanabilirsin
        Destroy(coin,2);
        Destroy(gameObject,2.1f);
    }


}
