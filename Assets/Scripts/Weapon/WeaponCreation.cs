using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponCreation : MonoBehaviour
{
    [SerializeField] private BallData ballData;

    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private List<Mesh> weapons=new List<Mesh>();

    [SerializeField] private WeaponData weaponData;
    private void Start() 
    {
        meshFilter.mesh=weapons[weaponData.index];
        Attack(FindObjectOfType<BossCharacter>());
    }

    private void Attack(BossCharacter bossCharacter)
    {
        //BUNU COLLIDER ILE YAP. VURUS HISSIYATINI VERMEK ICIN SART!
            transform.DOMove(bossCharacter.targetPos.position,.5f).OnComplete(()=>{
            ballData.damageAmount*=3;
            EventManager.Broadcast(GameEvent.OnBossDamage);
            bossCharacter.transform.DOPunchScale(new Vector3(10,10,10),2f,10,1);
            transform.DOScale(Vector3.zero,2f).OnComplete(()=>Destroy(gameObject));
        });
    }
}
