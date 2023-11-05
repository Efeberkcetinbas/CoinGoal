using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BodyAttack : MonoBehaviour,IBossAttack
{
    [SerializeField] private List<Transform> limbs=new List<Transform>();

    [SerializeField] private float x,y,z,duration,orderTime;

    //Boss Datadan cekebilirsin duration
    private List<Tween> tween=new List<Tween>();

    private void Start() 
    {
        //Kill counted Tweens
        tween=new List<Tween>(limbs.Count);
        for (int i = 0; i < limbs.Count; i++)
        {
            tween.Add(null);
        }
        Attack();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossDead,OnBossDead);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDead,OnBossDead);
    }
   
    public void Attack()
    {
        StartCoroutine(LimbsScaleUpDown());
    }

    public void OnBossDead()
    {
        for (int i = 0; i < tween.Count; i++)
        {
            tween[i].Kill();
        }
    }

    private IEnumerator LimbsScaleUpDown()
    {
        for (int i = 0; i < limbs.Count; i++)
        {
            yield return new WaitForSeconds(orderTime);
            tween[i]=limbs[i].DOScale(new Vector3(x,y,z),duration).OnComplete(()=>limbs[i].DOScale(Vector3.zero,.2f)).SetLoops(-1,LoopType.Yoyo);
        }
    }

    
}
