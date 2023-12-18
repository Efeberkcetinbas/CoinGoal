using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class BorderListControl : MonoBehaviour
{
    [SerializeField] private List<int> borderReq=new List<int>();
    [SerializeField] private List<GameObject> borders=new List<GameObject>();

    private int index=0;
    private int tempReq=0;
    [SerializeField] private int y;

    //[SerializeField] private List<TextMeshPro> borderReqTexts=new List<TextMeshPro>();

    public GameData gameData;

    private void Start() 
    {
        /*for (int i = 0; i < borderReq.Count; i++)
        {
            borderReqTexts[i].SetText("Open : "  + borderReq[i].ToString());
        }*/

        CalculateTemp();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
    }

    private void CalculateTemp()
    {
        tempReq=borderReq[index];
    }
    
    private void OnPassBetween()
    {
        if (index >= borderReq.Count)
        {
        // Handle the out-of-range condition, for example:
            //Debug.LogError("Index out of range");
            return;
        }

        tempReq--;

        //borderReqTexts[index].SetText("Open : " + tempReq.ToString());

        if (tempReq <= 0)
        {
            // Kapi Acilacak. Efektif yaparsin bunu
            gameData.BorderIndex=index;
            EventManager.BroadcastId(GameEvent.OnBordersDown,index);
            borders[index].transform.DOMoveY(y,2);
            index++;

            if (index < borderReq.Count)
            {
                // Only calculate temp and proceed if there are more borders
                Debug.Log("BALLS UNITED");
                EventManager.Broadcast(GameEvent.OnBallsUnited);
                CalculateTemp();
            }
            else
            {
                // All borders are open
                return;
            }
        }

    }


    private void OnRestartLevel()
    {
        for (int i = 0; i < borders.Count; i++)
        {
            borders[i].gameObject.SetActive(false);
        }

        index=0;
        borders[0].gameObject.SetActive(true);
        borders[0].transform.DOMoveY(2.12f,0.2f);
        CalculateTemp();
    }
}
