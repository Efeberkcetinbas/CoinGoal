using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestControl : MonoBehaviour
{
    [SerializeField] private Transform chest;

    [SerializeField] private float x,duration;

    [SerializeField] private Ease ease;


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
        chest.DORotate(new Vector3(x,0,0),duration).SetEase(ease);
    }
}
