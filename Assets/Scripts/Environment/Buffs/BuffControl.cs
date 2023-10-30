using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffControl : MonoBehaviour
{
    //Invincle, Destroyer

    
    public GameData gameData;
    public BallData ballData;
    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameStart,OnGameStart);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameStart,OnGameStart);
    }
    
    #region Buffs
    public void DoInvincle()
    {
        EventManager.Broadcast(GameEvent.OnInvulnerable);
        //Ekonomi lazim. Buff aldigimizda ucret artmali. 
        //Buff'in baslamasini game is Start eventine bagla
    }

    public void DoDestroyer()
    {
        EventManager.Broadcast(GameEvent.OnDestroyer);
    }

    #endregion

    private void OnGameStart()
    {
        StartCoroutine(DoReverseInvincible());
    }



    #region Caroutines
    //Sure baslangicini iyi ayarlamak lazim
    private IEnumerator DoReverseInvincible()
    {
        yield return new WaitForSeconds(gameData.BackTime);
        EventManager.Broadcast(GameEvent.OnVulnerable);
    }

    private IEnumerator DoReverseDestroyer()
    {
        yield return new WaitForSeconds(gameData.BackTime);
        EventManager.Broadcast(GameEvent.OnNormal);
    }
    #endregion
    
}
