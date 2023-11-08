using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMeshList : MonoBehaviour
{
    //Degisebilir
    public BallData ballData;

    [SerializeField] private List<Mesh> ballMeshs=new List<Mesh>();

    [SerializeField] private MeshFilter meshFilter;

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
        meshFilter.mesh=ballMeshs[ballData.selectedBallIndex];
    }
}
