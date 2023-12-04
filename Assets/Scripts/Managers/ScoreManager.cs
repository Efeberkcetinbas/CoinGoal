using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public GameData gameData;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.AddHandler(GameEvent.OnIncreaseGold,OnIncreaseGold);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.RemoveHandler(GameEvent.OnIncreaseGold,OnIncreaseGold);
    }
    private void OnIncreaseScore()
    {
        //gameData.score += 50;
        DOTween.To(GetScore,ChangeScore,gameData.score+gameData.increaseScore,.25f).OnUpdate(UpdateUI);
    }

    private int GetScore()
    {
        return gameData.score;
    }

    private void ChangeScore(int value)
    {
        gameData.score=value;
    }

    private void UpdateUI()
    {
        EventManager.Broadcast(GameEvent.OnUIUpdate);
    }

    //-----------------------------------------------------------------------------//

    private void OnIncreaseGold()
    {
        //gameData.score += 50;
        DOTween.To(GetDiamond,ChangeDiamond,gameData.diamond+gameData.increaseCoinAmount,.25f).OnUpdate(UpdateUIDiamond);
    }

    private int GetDiamond()
    {
        return gameData.diamond;
    }

    private void ChangeDiamond(int value)
    {
        gameData.diamond=value;
    }

    private void UpdateUIDiamond()
    {
        EventManager.Broadcast(GameEvent.OnUIDiamondUpdate);
    }
}
