using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public CinemachineVirtualCamera cm;

    [Header("Shake Control")]
    [SerializeField] private float shakeTime = 0.5f;
    [SerializeField] private float amplitudeGain=1;
    [SerializeField] private float frequencyGain=1;
    [SerializeField] private float newFieldOfView;
    [SerializeField] private float oldFieldOfView;
    private CinemachineBasicMultiChannelPerlin noise;

    [SerializeField] private List<Transform> balls=new List<Transform>();

    //Data
    public BallData ballData;


    private Transform Portal;

    
    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.AddHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.AddIdHandler(GameEvent.OnBordersDown,OnBordersDown);
        EventManager.AddHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        EventManager.AddHandler(GameEvent.OnTrapHitPlayer,OnTrapHitPlayer);
        EventManager.AddHandler(GameEvent.OnBossDead,OnBossDead);
        EventManager.AddHandler(GameEvent.OnUIBossUpdate,OnUIBossUpdate);
        EventManager.AddIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.AddHandler(GameEvent.OnSpecialTechnique,OnSpecialTechnique);
        EventManager.AddHandler(GameEvent.OnShopOpen,OnShopOpen);
        EventManager.AddHandler(GameEvent.OnShopClose,OnShopClose);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
        EventManager.RemoveIdHandler(GameEvent.OnBordersDown,OnBordersDown);
        EventManager.RemoveHandler(GameEvent.OnPortalOpen,OnPortalOpen);
        EventManager.RemoveHandler(GameEvent.OnTrapHitPlayer,OnTrapHitPlayer);
        EventManager.RemoveHandler(GameEvent.OnBossDead,OnBossDead);
        EventManager.RemoveHandler(GameEvent.OnUIBossUpdate,OnUIBossUpdate);
        EventManager.RemoveIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.RemoveHandler(GameEvent.OnSpecialTechnique,OnSpecialTechnique);
        EventManager.RemoveHandler(GameEvent.OnShopOpen,OnShopOpen);
        EventManager.RemoveHandler(GameEvent.OnShopClose,OnShopClose);
    }


    private void OnPassBetween()
    {
        Noise(amplitudeGain,frequencyGain,shakeTime);
        ChangeFieldOfViewHit(newFieldOfView,oldFieldOfView,shakeTime);
    }

    private void OnShopOpen()
    {
        ChangePriority(8);
    }

    private void OnShopClose()
    {
        ChangePriority(11);
    }
    
    private void OnHitWall(int id)
    {
        ChangeFieldOfViewHit(75,80,0.25f);
    }

    private void OnSpecialTechnique()
    {
        Noise(5,5,1);
    }

    private void OnSpawnWeapon()
    {
        Noise(1,1,0.1f);
    }

    private void OnTrapHitPlayer()
    {
        Noise(5,5,2.5f);
    }

    private void OnBallIndexIncrease()
    {
        ChangeFollow(balls[ballData.currentBallIndex].transform);
    }


    private void OnNextLevel()
    {
        ChangeFieldOfView(80,5);
        ChangeFollow(balls[ballData.currentBallIndex].transform);
    }

    private void OnBossDead()
    {
        Noise(5,5,1.75f);
    }

    private void OnPortalOpen()
    {
        Portal=FindObjectOfType<PortalControl>().transform;
        ChangeFieldOfView(40,5);
        ChangeFollow(Portal);
    }

    private void OnBordersDown(int id)
    {
        Noise(3,3,1f);
    }

    private void OnUIBossUpdate()
    {
        Noise(4,4,0.5f);
    }
    

    private void Start() 
    {
        noise=cm.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        if(noise == null)
            Debug.LogError("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {noise}");

        OnBallIndexIncrease();
    }

    private void Noise(float amplitudeGain,float frequencyGain,float shakeTime) 
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        StartCoroutine(ResetNoise(shakeTime));    
    }

    private IEnumerator ResetNoise(float duration)
    {
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;    
    }
    public void ChangeFieldOfView(float fieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration);
    }

    

    public void ChangeFieldOfViewHit(float newFieldOfView, float oldFieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, newFieldOfView, duration).OnComplete(()=>{
            DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, oldFieldOfView, duration);
        });
    }

    public void ChangeFollow(Transform Ball)
    {
        cm.m_Follow=Ball;
    }

    private void ChangePriority(int val)
    {
        cm.m_Priority=val;
    }
}
