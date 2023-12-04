using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestControl : MonoBehaviour
{
    [SerializeField] private Transform chest;

    [SerializeField] private float x,duration;

    [SerializeField] private Ease ease;

    [SerializeField] private List<Transform> CoinAndDiamond=new List<Transform>();

    private WaitForSeconds waitForSeconds;

    private BallController ballController;

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(1f);
        ballController=FindObjectOfType<BallController>();
    }


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGoal,OnGoal);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGoal,OnGoal);
        
    }

    private void OnGoal()
    {
        chest.DORotate(new Vector3(x,0,0),duration).SetEase(ease).OnComplete(()=>StartCoroutine(Throw()));
    }


    private IEnumerator Throw()
    {
        for (int i = 0; i < CoinAndDiamond.Count; i++)
        {
            CoinAndDiamond[i].gameObject.SetActive(true);
            CoinAndDiamond[i].DOJump(ballController.balls[i].transform.position,2,1,0.5f);
            yield return waitForSeconds;
        }
        yield return waitForSeconds;
        EventManager.Broadcast(GameEvent.OnPortalOpen);
    }
}
