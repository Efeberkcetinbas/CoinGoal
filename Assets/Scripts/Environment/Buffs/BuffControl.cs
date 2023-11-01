using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffControl : MonoBehaviour
{
    //Invincle, Destroyer

    
    public GameData gameData;
    public BallData ballData;

    private bool isBuffused=false;

    [SerializeField] private bool isSpeedUp,isDestroyer,isInvulnerable=false;


    [SerializeField] private GameObject buffBar;
    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnNextLevel()
    {
        isBuffused=false;
    }

    #region Buffs Panel

    public void SetDestroyer()
    {
        isDestroyer=true;
        isBuffused=true;
    }
    public void SetSpeedUp()
    {
        isSpeedUp=true;
        isBuffused=true;
    }
    public void SetInvulnerable()
    {
        isInvulnerable=true;
        isBuffused=true;
    }

    
    

    #endregion 

    
    #region Buffs
    private void DoInvincle()
    {
        isBuffused=true;
        EventManager.Broadcast(GameEvent.OnInvulnerable);
        StartCoroutine(DoReverse(GameEvent.OnVulnerable));

        //Ekonomi lazim. Buff aldigimizda ucret artmali. 
        //Buff'in baslamasini game is Start eventine bagla
    }

    private void DoDestroyer()
    {
        isBuffused=true;
        EventManager.Broadcast(GameEvent.OnDestroyer);
        StartCoroutine(DoReverse(GameEvent.OnNormal));
    }

    private void DoSpeedUp()
    {
        isBuffused=true;
        EventManager.Broadcast(GameEvent.OnSpeedUp);
        StartCoroutine(DoReverse(GameEvent.OnSpeedNormal));
    }

    #endregion

    private void OnGameStart()
    {
        if(isBuffused)
        {
            if(isDestroyer) DoDestroyer();
            if(isInvulnerable) DoInvincle();
            if(isSpeedUp) DoSpeedUp();
            
            buffBar.SetActive(true);
            EventManager.Broadcast(GameEvent.OnUpdateBuff);
        }
    }

    



    #region Caroutines
    //Sure baslangicini iyi ayarlamak lazim
    private IEnumerator DoReverse(GameEvent gameEvent)
    {
        yield return new WaitForSeconds(gameData.BackTime);
        EventManager.Broadcast(gameEvent);
    }
    #endregion
    
}
