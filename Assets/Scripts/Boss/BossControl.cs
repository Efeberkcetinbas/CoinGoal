using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class BossControl : MonoBehaviour
{
    public BossData bossData;

    [SerializeField] private RectTransform bossImage;
    [SerializeField] private TextMeshProUGUI bossText;
    [SerializeField] private Image bossProgressBar;

    [SerializeField] private GameObject BossUI;

    [SerializeField] private float y_axis;


    [SerializeField] private GameObject[] bars;
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossActive,OnBossActive);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossActive,OnBossActive);
    }


    private void OnBossActive()
    {
        BossUI.SetActive(true);
        BossUI.transform.DOScale(Vector3.one*2,1f).OnComplete(()=>{
            BossUI.transform.DOScale(Vector3.one,1f).OnComplete(()=>{
                bossImage.DOAnchorPosY(y_axis,1);
                for (int i = 0; i < bars.Length; i++)
                {
                    bars[i].SetActive(true);
                }
                bossProgressBar.fillAmount=0;
                bossProgressBar.DOFillAmount(1,1);
            });
        });
        bossText.SetText(bossData.Name);
        
        
        
        
        //Cool bir sekilde sahneye gelir
    }
}
