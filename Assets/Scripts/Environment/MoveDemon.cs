using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveDemon : MonoBehaviour
{
    [SerializeField] private int demonRequire;
    private int tempRequire;

    private WaitForSeconds waitForSeconds;
    [SerializeField] private TextMeshPro demonText;

    private bool isUnite=false;
    private bool isFinish=false;


    private void Start()
    {
        waitForSeconds=new WaitForSeconds(3);
        tempRequire=demonRequire;
        SetDemonText();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.AddHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.AddHandler(GameEvent.OnBallsDivided,OnBallsDivided);
        EventManager.AddHandler(GameEvent.OnBallsUnited,OnBallsUnited);
        EventManager.AddHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.AddHandler(GameEvent.OnOpenLevelFromPanel,OnOpenLevelFromPanel);

    }


    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnTouchEnd,OnTouchEnd);
        EventManager.RemoveHandler(GameEvent.OnBallsDivided,OnBallsDivided);
        EventManager.RemoveHandler(GameEvent.OnBallsUnited,OnBallsUnited);
        EventManager.RemoveHandler(GameEvent.OnRestartLevel,OnRestartLevel);
        EventManager.RemoveHandler(GameEvent.OnOpenLevelFromPanel,OnOpenLevelFromPanel);

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

    private void OnRestartLevel()
    {
        demonRequire=tempRequire;
        SetDemonText();
        isFinish=false;
    }

    private IEnumerator CheckForPass()
    {
        yield return waitForSeconds;
        if(demonRequire<0 && !isFinish)
        {
            EventManager.Broadcast(GameEvent.OnTrapHitPlayer);
            isFinish=true;
        }
            

    }

    private void OnOpenLevelFromPanel()
    {
        OnRestartLevel();
    }

    private void SetDemonText()
    {
        demonText.SetText(demonRequire.ToString());
    }
}
