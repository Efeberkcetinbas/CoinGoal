using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveDemon : MonoBehaviour
{
    [SerializeField] private int demonRequire;

    private WaitForSeconds waitForSeconds;
    [SerializeField] private TextMeshPro demonText;

    private bool isUnite=false;


    private void Start()
    {
        waitForSeconds=new WaitForSeconds(3);
        SetDemonText();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.AddHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.AddHandler(GameEvent.OnBallsDivided,OnBallsDivided);
        EventManager.AddHandler(GameEvent.OnBallsUnited,OnBallsUnited);

    }


    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.RemoveHandler(GameEvent.OnBallsDivided,OnBallsDivided);
        EventManager.RemoveHandler(GameEvent.OnBallsUnited,OnBallsUnited);

    }


    private void OnPassBetween()
    {
        if(!isUnite)
        {
            demonRequire++;
            SetDemonText();
        }
        
    }

    private void OnTouchEnd()
    {
        if(!isUnite)
        {
            demonRequire--;
            SetDemonText();
            StartCoroutine(CheckForPass());
        }
        
    }

    private void OnBallsDivided()
    {
        isUnite=false;
    }

    private void OnBallsUnited()
    {
        isUnite=true;
    }


    private IEnumerator CheckForPass()
    {
        yield return waitForSeconds;
        if(demonRequire<0)
            EventManager.Broadcast(GameEvent.OnTrapHitPlayer);

    }

    private void SetDemonText()
    {
        demonText.SetText(demonRequire.ToString());
    }
}
