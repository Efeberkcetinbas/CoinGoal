using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMeshList : MonoBehaviour
{
    //Degisebilir
    public BallData ballData;

    [SerializeField] private List<Mesh> ballMeshs=new List<Mesh>();
    [SerializeField] private List<GameObject> balls=new List<GameObject>();


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
        //-!!!!!!!!!!!!! MATERYAL ATLASI OLAN MODELLER OLDUGU ZAMAN BUNU KULLAN. HAZIR MODEL YUZUNDEN MESH FILTER MATERYAL DEGISMIYOR
        //meshFilter.mesh=ballMeshs[ballData.selectedBallIndex];
        
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].SetActive(false);   
        }

        balls[ballData.selectedBallIndex].SetActive(true);
    }
}
