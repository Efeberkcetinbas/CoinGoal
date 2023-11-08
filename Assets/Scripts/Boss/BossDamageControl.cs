using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossDamageControl : Obstacleable
{
    public BossData bossData;
    public BallData ballData;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ParticleSystem bossParticle,thunderParticle;

    private bool isDead=false;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossDamage,OnBossDamage);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDamage,OnBossDamage);
        
    }

    private void Start() 
    {
        bossData.TempHealth=bossData.Health;
    }
    internal override void DoAction(Player player)
    {
        if(player.isOrderMe)
        {
            GetDamage(0.2f,bossParticle);
        }
        

        
    }


    private void GetDamage(float time,ParticleSystem particle)
    {
        bossData.Health-= ballData.damageAmount;
        particle.Play();
        StartCoroutine(ChangeColor(time));
        EventManager.Broadcast(GameEvent.OnUIBossUpdate);

        if(bossData.Health<=0 && !isDead)
        {
            EventManager.Broadcast(GameEvent.OnBossDead);
            isDead=true;
                
        }
    }

    private IEnumerator ChangeColor(float time)
    {
        meshRenderer.material.color=Color.red;
        //particle.Play();
        //StartPointMove();
        transform.DOScale(Vector3.one/1.3f,time).OnComplete(()=>{
            transform.DOScale(Vector3.one,time);
        });

        yield return new WaitForSeconds(time);
        meshRenderer.material.color=Color.white;
    }

    private void OnBossDamage()
    {
        GetDamage(1,thunderParticle);
        ballData.damageAmount=ballData.damageAmount/3;
    }
    //Damage aldiginda kirmizi bir sekilde ciksin
    /*
    internal void XPEffect()
    {
        //BUNU 3UNDE DE YAPIYOR. ONA DIKKAT ETMEK LAZIM. BALL CONTROLLERDEN YAZ

        GameObject XP=Instantiate(scoreXP,transform.position,Quaternion.identity);
        XP.transform.DOLocalJump(XP.transform.localPosition,1,1,1,false);
        XP.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + gameData.increaseScore.ToString();
        XP.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>XP.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(XP,2);
    }*/
}
