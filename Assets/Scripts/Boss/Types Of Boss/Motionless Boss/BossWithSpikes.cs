using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossWithSpikes : MonoBehaviour,IBossMove
{

    [SerializeField] private List<Transform> spikes=new List<Transform>();

    [SerializeField] private float y,oldy,duration,orderTime;

    private List<Tween> tween=new List<Tween>();

    private void Start() 
    {
        tween=new List<Tween>(spikes.Count);
        for (int i = 0; i < spikes.Count; i++)
        {
            tween.Add(null);
        }
        Move();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossDead,OnBossDead);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDead,OnBossDead);
    }

    public void OnBossDead()
    {
        for (int i = 0; i < tween.Count; i++)
        {
            tween[i].Kill();
            Debug.Log("MOVE");
        }
        
    }

    public void Move()
    {
        StartCoroutine(SpikesMoveOrdinary());
    }

    private IEnumerator SpikesMoveOrdinary()
    {
        for (int i = 0; i < spikes.Count; i++)
        {
            
            tween[i]=spikes[i].DOLocalMoveY(y,duration).OnComplete(()=>spikes[i].DOLocalMoveY(oldy,duration)).SetLoops(-1,LoopType.Yoyo);
            yield return new WaitForSeconds(orderTime);
        }
    }

   
}
