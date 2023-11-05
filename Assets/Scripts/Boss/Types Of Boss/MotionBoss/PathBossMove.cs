using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathBossMove : MonoBehaviour,IBossMove
{
    [SerializeField] private PathType pathType;

    [SerializeField] private Vector3 [] pathPoints;

    private Tween tween;
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;


    private void Start() 
    {
        Move();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossDead,OnBossDead);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDead,OnBossDead);
    }

    public void OnBossDead()
    {
        tween.Kill();
    }
    public void Move()
    {
        tween=transform.DOPath(pathPoints,duration,pathType);
        tween.SetEase(ease).SetLoops(-1);
    }

    
}
