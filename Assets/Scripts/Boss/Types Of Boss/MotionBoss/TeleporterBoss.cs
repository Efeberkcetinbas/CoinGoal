using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TeleporterBoss : MonoBehaviour, IBossMove
{
    [SerializeField] private List<Transform> points=new List<Transform>();

    private int index;
    private int randomPos;
    [SerializeField] private float moveTime;

    [SerializeField] private ParticleSystem PortalEffect;

    [SerializeField] private bool isRandom=false;

    private List<Tween> tween=new List<Tween>();

    private void Start() 
    {
        tween=new List<Tween>(2);
        for (int i = 0; i < 2; i++)
        {
            tween.Add(null);
        }
        InvokeRepeating("Move",3,moveTime);
    }

     private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossDead,OnBossDead);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDead,OnBossDead);
    }


    public void Move()
    {
        StartCoroutine(CallMove());
    }

    public void OnBossDead()
    {
        for (int i = 0; i < tween.Count; i++)
        {
            tween[i].Kill();
        }
        
        CancelInvoke();
    }

    

    private IEnumerator CallMove()
    {
        RandomPos();
        //yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(2);
        PlayParticleEffect();
        tween[0]=transform.DOScale(new Vector3(0.1f,0.1f,0.1f),0.25f).OnComplete(()=>{
            Movement();
        });
    }
    public void Movement()
    {
        //Sadece sahnedeki groudlarin uzerinde olsun.
        transform.position=points[randomPos].transform.position;
        tween[1]=transform.DOScale(new Vector3(1,1,1),0.3f);
    }

    public void PlayParticleEffect()
    {
        /*PortalEffect.transform.position=new Vector3(points[randomPos].transform.position.x,
        points[randomPos].transform.position.y+1,points[randomPos].transform.position.z);*/
        PortalEffect.Play();


        /*Instantiate(PortalEffect,new Vector3(points[randomPos].transform.position.x,
        points[randomPos].transform.position.y+1,points[randomPos].transform.position.z),points[randomPos].transform.rotation);*/
    }

    

    private int RandomPos()
    {
        //Bazen surekli ayni konum uzerinde donuyor. Random yerine index arttir.
        if(index<points.Count-1)
        {
            index++;
        }
        else
        {
            index=0;
        }
        //randomPos=Random.Range(0,index);
        if(isRandom)
            randomPos=Random.Range(0,points.Count);
        else
            randomPos=index;
        
        Debug.Log(randomPos);
        return randomPos;
    }
}
