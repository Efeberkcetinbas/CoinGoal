using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class BallLineCollision : MonoBehaviour
{
    public Transform gapStart; // The transform of the starting point of the gap
    public Transform gapEnd;   // The transform of the ending point of the gap


    private LineRenderer lineRenderer;

    [SerializeField] private int ID1,ID2;



    public BallData ballData;
    public GameData gameData;

    [SerializeField] private LayerMask layerMask;
    

    private void Start() 
    {
        lineRenderer=GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(!ballData.isItPassed && !gameData.isGameEnd)
            CalculateIntersection();
    }

    private void CalculateIntersection()
    {
        int numberOfPositions = lineRenderer.positionCount;
        Vector3[] positions = new Vector3[numberOfPositions];
        lineRenderer.GetPositions(positions);

        for (int i = 0; i < numberOfPositions - 1; i++)
        {
            Vector3 startPoint = positions[i];
            Vector3 endPoint = positions[i + 1];
            Vector3 direction = endPoint - startPoint;
            float distance = direction.magnitude;

            // Cast a ray along the line segment
            RaycastHit hit;
            if (Physics.Raycast(startPoint, direction, out hit, distance, layerMask))
            {
                //GetComponent Yerine Daha Optimize bir cozum var mi?
                if(hit.collider.CompareTag("Player"))
                {
                    if (hit.collider.GetComponent<BallIdentifier>().BallID != ID1 && hit.collider.GetComponent<BallIdentifier>().BallID != ID2) 
                    {
                        //Debug.Log("HIT COLLIDE : " + hit.collider.name);
                    // A collision has occurred with an object that meets the criteria
                        if(!gameData.isMiniGame)
                        {
                            
                            if(gameData.canChangeIndex && gameData.canIntersect)
                            {
                                EventManager.Broadcast(GameEvent.OnPassBetween);
                                hit.collider.GetComponent<Player>().XPEffect();
                                ballData.isItPassed=true;
                                //StartCoroutine(LineEffect());
                            }
                            
                        }
                        else
                        {
                            if(gameData.canChangeIndex && gameData.canIntersect)
                            {
                                //Debug.Log("PASS");
                                ballData.isItPassed=true;
                                EventManager.Broadcast(GameEvent.OnMiniGamePasses);
                                //StartCoroutine(LineEffect());
                            }
                            
                        }
                        
                    }
                }
               
            }
        }
    }

    

    private IEnumerator LineEffect()
    {
        lineRenderer.enabled=true;
        yield return new WaitForSeconds(0.25f);
        lineRenderer.enabled=false;
    }
}
