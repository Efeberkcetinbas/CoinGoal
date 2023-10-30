using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BorderListControl : MonoBehaviour
{
    [SerializeField] private List<int> borderReq=new List<int>();
    [SerializeField] private List<GameObject> borders=new List<GameObject>();

    private int index=0;
    private int tempReq=0;

    [SerializeField] private List<TextMeshPro> borderReqTexts=new List<TextMeshPro>();

    private void Start() 
    {
        for (int i = 0; i < borderReq.Count; i++)
        {
            borderReqTexts[i].SetText("Open : "  + borderReq[i].ToString());
        }

        CalculateTemp();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPassBetween,OnPassBetween);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
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
            Debug.LogError("Index out of range");
            return;
        }

        tempReq--;

        borderReqTexts[index].SetText("Open : " + tempReq.ToString());

        if (tempReq <= 0)
        {
            // Kapi Acilacak. Efektif yaparsin bunu
            borders[index].SetActive(false);
            index++;

            if (index < borderReq.Count)
            {
                // Only calculate temp and proceed if there are more borders
                CalculateTemp();
            }
            else
            {
                // All borders are open
                return;
            }
        }

    }
}
