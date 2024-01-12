using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMeshList : MonoBehaviour
{
    //Degisebilir
    public BallData ballData;

    [SerializeField] private List<Texture> balls=new List<Texture>();


    [SerializeField] private MeshRenderer meshRenderer;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBallMeshChange,OnBallMeshChange);

    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBallMeshChange,OnBallMeshChange);
        
    }
    private void Start() 
    {
        OnBallMeshChange();
    }


    private void OnBallMeshChange()
    {
        meshRenderer.material.mainTexture=balls[ballData.selectedBallIndex];
    }
}
