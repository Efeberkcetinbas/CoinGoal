using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponCreation : MonoBehaviour
{
    [SerializeField] private BallData ballData;

    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private List<Mesh> weapons=new List<Mesh>();
    [SerializeField] private ParticleSystem weaponParticle;

    [SerializeField] private WeaponData weaponData;

    private WaitForSeconds waitForSeconds;


    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(3);
        meshFilter.mesh=weapons[weaponData.index];
        Attack();
    }

    private void Attack()
    {
        ballData.damageAmount*=3;
        transform.DOScale(Vector3.one*1.25f,1f).OnComplete(()=>transform.DOScale(Vector3.zero,1f));
        transform.DOMoveY(transform.localPosition.y+3,0.5f);
        EventManager.Broadcast(GameEvent.OnBossDamage);
        weaponParticle.Play();
        StartCoroutine(DestroyWeapon());
    }

    private IEnumerator DestroyWeapon()
    {
        yield return waitForSeconds;
        Destroy(gameObject);
    }



}
