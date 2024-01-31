using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Opening : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private List<GameObject> meshObject;
    [SerializeField] private List<Vector3> meshObjectScales;
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

    private void Start()
    {
        for (int i = 0; i < meshObject.Count; i++)
        {
            meshObjectScales.Add(Vector3.zero);
        }
    }

    private void OnGameStart()
    {
        for (int i = 0; i < meshObject.Count; i++)
        {
            meshObject[i].SetActive(true);
            
            meshObjectScales[i]=meshObject[i].transform.localScale;
            meshObject[i].transform.localScale=Vector3.zero;
            meshObject[i].transform.DOScale(meshObjectScales[i],0.5f).SetEase(ease);
        }
    }

    private void OnPortalOpen()
    {
        /*transform.DOScale(Vector3.zero,0.2f).OnComplete(()=>{
            for (int i = 0; i < meshObject.Count; i++)
            {
                meshObject[i].SetActive(false);
            }
        });*/
        
        for (int i = 0; i < meshObject.Count; i++)
        {
            meshObject[i].SetActive(false);
        }
    }
}
