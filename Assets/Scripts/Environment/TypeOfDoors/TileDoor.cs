using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDoor : DoorButtonControl
{
    [SerializeField] private int ID;
    [SerializeField] private int sizeX,sizeZ;

    [SerializeField] private int increaseX,increaseZ;
     
    [SerializeField] private GameObject tile;

    [SerializeField] private float y,duration;

    private WaitForSeconds waitForSeconds;

    [SerializeField] private Transform parent;

    private void Start() 
    {
        waitForSeconds=new WaitForSeconds(duration);
        
    }
    internal override void OnOpenButton(int id)
    {
        if(ID==id)
        {
            StartCoroutine(BuildBridge(sizeX,sizeZ));
        }
    }

    private IEnumerator BuildBridge(int size_x,int size_y)
    {
        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                Instantiate(tile,new Vector3(parent.localPosition.x + i+increaseX,y,parent.localPosition.z + j+increaseZ),Quaternion.identity,parent);
                EventManager.Broadcast(GameEvent.OnBridgeOpen);
                yield return waitForSeconds;
            }
        }
    }
}
