using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BreakeableWall : Obstacleable
{
    [SerializeField] private List<GameObject> wallList=new List<GameObject>();

    [SerializeField] private BallData ballData;
    private int index;
    private int ID;

    private WaitForSeconds waitForSeconds;

    [SerializeField] private ParticleSystem damageParticle;

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(1);
        
    }
    public BreakeableWall()
    {
        canStay=false;
        canDamageToPlayer=false;
    }


    internal override void DoAction(Player player)
    {
        if (ballData.BallSpeed > 10)
        {
            if (index < wallList.Count)
            {
                //Daha optimize count atayip sonra yazmak

                ID=player.ID;
                int children = wallList[index].transform.childCount;
                damageParticle.Play();
                EventManager.BroadcastId(GameEvent.OnHitWall,ID);
                for (int i = 0; i < children; i++)
                {
                    wallList[index].transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                    StartCoroutine(SetChildrenLost(children,index,wallList));
                    //Sahnenin Altina kayip destroy islemi.
                }
                index++;
                if(index==wallList.Count)
                {
                    Destroy(gameObject,5);
                }
            }
            
        }
    }

    private IEnumerator SetChildrenLost(int child,int index, List<GameObject> gameObjects)
    {
        yield return waitForSeconds;
        for (int i = 0; i < child; i++)
        {
            //Daha iyi bir sekilde goster bunu
            gameObjects[index].transform.DOScale(Vector3.zero,3f).OnComplete(()=>{
                Destroy(gameObjects[index]);
                //Other Stuff if necessary (sorry for both turkish and english comment bilal emmi)
            });
        }
    }

    
}
