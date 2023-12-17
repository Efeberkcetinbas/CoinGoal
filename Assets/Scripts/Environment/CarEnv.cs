using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class CarData
{
    public GameObject car;
    public float duration;
}
public class CarEnv : MonoBehaviour
{
    [SerializeField] private List<CarData> cars= new List<CarData>();
    [SerializeField] private Transform startPos,endPos;

    private int index;

    [SerializeField]private bool canWork=true;

    private Tween tween;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        
    }


    private void OnGameStart()
    {
        canWork=true;
        CheckWork();
    }

    private void OnPortalOpen()
    {
        canWork=false;
        DOTween.Kill(tween);
    }

    private void CheckWork()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].car.transform.localPosition=startPos.position;
            cars[i].car.SetActive(false);
        }

        cars[index].car.SetActive(true);
        tween=cars[index].car.transform.DOLocalMove(endPos.position,cars[index].duration).OnComplete(()=>{
            NextCar();
        });
    }

    private void NextCar()
    {
        if(canWork)
        {
            if(index>=cars.Count)
            {
                index=0;
            }
            index++;
            CheckWork();
        }
    }
}
