using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameEvent
{
    //Player
    OnPassBetween,
    OnTrapHitPlayer,
    OnDamagePlayer,
    OnPlayerDead,
    OnBallsUnited,
    OnBallsDivided,
    OnResetBallsPosition,


    //EndGame
    OnPortalOpen,
    

    OnMiniGameBall,
    


    //Buffs
    OnOpenBuffPanel,
    OnUpdateBuff,
    OnInvulnerable,
    OnVulnerable,
    OnDestroyer,
    OnNormal,
    OnSpeedUp,
    OnSpeedNormal,
    OnBoughtBuff,

    

    //Ball
    OnBallIndexIncrease,

    //Environment
    OnOpenButton,
    OnCloseButton,
    OnOpenTraps,
    OnCloseTraps,
    OnBridgeOpen,
    OnBarrel,
    OnWindSound,
    OnCollect,
    OnSticky,

    //Incrementals
    OnOpenIncrementalPanel,

    //Level System
    OnOpenLevelSystem,

    
    OnHitWall,
    OnUpPortal,

    //Mini Game
    OnMiniGameFinish,
    OnMiniGameUIUpdate,
    OnMiniGamePasses,
    OnMiniGameActive,
    OnGoal,

    //Levels
    OnBordersDown,

    //Touch
    OnTouchStart,
    OnTouchEnd,

    //Panel

    //Shopping
    OnShopBallSelected,
    OnBallMeshChange,
    OnShopOpen,
    OnShopClose,


    //Game Management
    OnIncreaseScore,
    OnButtonClicked,
    OnIncreaseGold,
    OnUIUpdate,
    OnUIDiamondUpdate,
    OnUIRequirementUpdate,
    OnUIGameOver,
    OnNextLevel,
    OnLoadNextLevel,
    OnRestartLevel,
    OnGameStart,
    OnGameOver
}
public class EventManager
{
    private static Dictionary<GameEvent,Action> eventTable = new Dictionary<GameEvent, Action>();
    
    private static Dictionary<GameEvent,Action<int>> IdEventTable=new Dictionary<GameEvent, Action<int>>();
    //2 parametre baglayacagimiz ile bagladigimiz

    
    public static void AddHandler(GameEvent gameEvent,Action action)
    {
        if(!eventTable.ContainsKey(gameEvent))
            eventTable[gameEvent]=action;
        else eventTable[gameEvent]+=action;
    }

    public static void RemoveHandler(GameEvent gameEvent,Action action)
    {
        if(eventTable[gameEvent]!=null)
            eventTable[gameEvent]-=action;
        if(eventTable[gameEvent]==null)
            eventTable.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent gameEvent)
    {
        if(eventTable[gameEvent]!=null)
            eventTable[gameEvent]();
    }

    public static void AddIdHandler(GameEvent gameIdEvent,Action<int> actionId)
    {
        if(!IdEventTable.ContainsKey(gameIdEvent))
            IdEventTable[gameIdEvent]=actionId;
        else IdEventTable[gameIdEvent]+=actionId;
    }

    public static void RemoveIdHandler(GameEvent gameIdEvent,Action<int> actionId)
    {
        if(IdEventTable[gameIdEvent]!=null)
            IdEventTable[gameIdEvent]-=actionId;
        if(IdEventTable[gameIdEvent]==null)
            IdEventTable.Remove(gameIdEvent);
    }

    public static void BroadcastId(GameEvent gameIdEvent,int id)
    {
        if(IdEventTable[gameIdEvent]!=null)
            IdEventTable[gameIdEvent](id);
    }
    
}
