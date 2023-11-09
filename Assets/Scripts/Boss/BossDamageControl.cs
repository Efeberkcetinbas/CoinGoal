using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossDamageControl : Obstacleable
{
    public BossData bossData;
    public BallData ballData;
    public WeaponData weaponData;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ParticleSystem bossParticle;

    [SerializeField] private List<ParticleSystem> specialDamageParticles;

    private bool isDead=false;

    [Header("Special Effect Values")]
    [SerializeField] private Transform character;
    [SerializeField] private float scaleVal;
    [SerializeField] private float duration;
    [SerializeField] private int vibrato;


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
        transform.DOScale(Vector3.one/1.3f,time).OnComplete(()=>{
            transform.DOScale(Vector3.one,time);
        });

        yield return new WaitForSeconds(time);
        meshRenderer.material.color=Color.white;
    }

    private void OnBossDamage()
    {
        GetDamage(1,specialDamageParticles[weaponData.weaponIndexParticle]);
        character.DOPunchScale(new Vector3(scaleVal,scaleVal,scaleVal),duration,vibrato,1);
        ballData.damageAmount=ballData.damageAmount/3;
    }

}
