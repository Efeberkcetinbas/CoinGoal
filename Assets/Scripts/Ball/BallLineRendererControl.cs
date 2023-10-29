using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLineRendererControl : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    [SerializeField] private Transform ball1,ball2;

    //Sira hangisine gecerse onu acariz

    private void Start() 
    {
        line.positionCount=2;
    }
    //Bu scripte gerek yok ama rendereri dogru olcmek icin dogrulama amacli kullaniyorum. Ilerde scripti kaldiririm.
    private void Update() 
    {
        line.SetPosition(0,ball1.position);
        line.SetPosition(1,ball2.position);

    }

    
    
}
