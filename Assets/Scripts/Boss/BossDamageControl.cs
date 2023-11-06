using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossDamageControl : Obstacleable
{
    public BossData bossData;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ParticleSystem bossParticle;

    private void Start() 
    {
        bossData.TempHealth=bossData.Health;
    }
    internal override void DoAction(Player player)
    {
        bossData.Health--;
        bossParticle.Play();
        StartCoroutine(ChangeColor());
        EventManager.Broadcast(GameEvent.OnUIBossUpdate);

        if(bossData.Health<=0)
        {

            EventManager.Broadcast(GameEvent.OnBossDead);
            //Particle
            //Camera Shake
            //3 Balls gibi yap
            
        }
    }

    private IEnumerator ChangeColor()
    {
        meshRenderer.material.color=Color.red;
        //particle.Play();
        //StartPointMove();
        transform.DOScale(Vector3.one/1.3f,0.2f).OnComplete(()=>{
            transform.DOScale(Vector3.one,0.2f);
        });

        yield return new WaitForSeconds(0.2f);
        meshRenderer.material.color=Color.white;
    }
}
