using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TeleporterBoss : MonoBehaviour, IBossMove
{
    [SerializeField] private List<Transform> points=new List<Transform>();

    private int index;
    private int randomPos;

    [SerializeField] private ParticleSystem PortalEffect;


    private void Start() 
    {
        InvokeRepeating("Move",3,5);
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
        CancelInvoke();
    }

    

    private IEnumerator CallMove()
    {
        RandomPos();
        //yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(2);
        PlayParticleEffect();
        transform.DOScale(new Vector3(0.1f,0.1f,0.1f),0.25f).OnComplete(()=>{
            Movement();
        });
    }
    public void Movement()
    {
        //Sadece sahnedeki groudlarin uzerinde olsun.
        transform.position=points[randomPos].transform.position;
        transform.DOScale(new Vector3(1,1,1),0.3f);
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
        randomPos=index;
        Debug.Log(randomPos);
        return randomPos;
    }
}
