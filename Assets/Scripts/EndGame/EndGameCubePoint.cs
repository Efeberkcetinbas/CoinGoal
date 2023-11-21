using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameCubePoint : Obstacleable
{
    [SerializeField] private int pointVal;
    [SerializeField] private TextMeshPro pointTex;

    private void Start() 
    {
        pointTex.SetText(pointVal.ToString());
    }
    public EndGameCubePoint()
    {
        canStay=false;
        canDamageToPlayer=false;
    }


    internal override void DoAction(Player player)
    {
        Debug.Log("POINT " + pointVal);
        //gameObject.SetActive(false);
    }
}
