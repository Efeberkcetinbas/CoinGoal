using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,CharacterPanel,WeaponPanel;


    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,CharacterX,CharacterY,WeaponX,WeaponY,duration;

    public GameData gameData;


    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }
    
    public void StartGame() 
    {
        gameData.isGameEnd=false;
        StartPanel.gameObject.SetActive(false);
    }

    private void OnNextLevel()
    {
        StartPanel.gameObject.SetActive(true);
        StartPanel.DOAnchorPos(Vector2.zero,0.1f);
        StartCoroutine(Blink(Fade.gameObject,Fade));
    }


  

    private IEnumerator Blink(GameObject gameObject,Image image)
    {
        
        gameObject.SetActive(true);
        image.color=new Color(0,0,0,1);
        image.DOFade(0,0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);

    }


    public void OpenCharacterPanel()
    {
        EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        CharacterPanel.gameObject.SetActive(true);
        CharacterPanel.DOAnchorPos(Vector2.zero,duration);
    }

    public void OpenWeaponPanel()
    {
        EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        WeaponPanel.gameObject.SetActive(true);
        WeaponPanel.DOAnchorPos(Vector2.zero,duration);
    }

    public void BackToStart(bool isOnCharacter)
    {

        if(isOnCharacter)
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            CharacterPanel.DOAnchorPos(new Vector2(CharacterX,CharacterY),duration);
            //.OnComplete(()=>CharacterPanel.gameObject.SetActive(false));
        }
        else
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            WeaponPanel.DOAnchorPos(new Vector2(WeaponX,WeaponY),duration);
            //.OnComplete(()=>WeaponPanel.gameObject.SetActive(false));
        }

        EventManager.Broadcast(GameEvent.OnButtonClicked);

    }

}
