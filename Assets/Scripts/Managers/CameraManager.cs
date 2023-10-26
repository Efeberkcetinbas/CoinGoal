using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public CinemachineVirtualCamera cm;

    [Header("Shake Control")]
    public float shakeTime = 0.5f;
    public float amplitudeGain=1;
    public float frequencyGain=1;
    private CinemachineBasicMultiChannelPerlin noise;

    [SerializeField] private List<Transform> balls=new List<Transform>();

    //Data
    public BallData ballData;


    


    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.AddHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnPassBetween,OnPassBetween);
        EventManager.RemoveHandler(GameEvent.OnBallIndexIncrease,OnBallIndexIncrease);
    }

    private void OnGoal()
    {
        Noise(amplitudeGain,frequencyGain,shakeTime);
    }

    private void OnPassBetween()
    {
        Noise(2,2,0.2f);
        ChangeFieldOfViewHit(4,5,0.2f);
    }

    private void OnSpawnWeapon()
    {
        Noise(1,1,0.1f);
    }

    private void OnBallIndexIncrease()
    {
        ChangeFollow(balls[ballData.currentBallIndex].transform);
    }


    private void OnNextLevel()
    {
        ChangeFieldOfView(5,0.1f);
    }

    

    private void Start() 
    {
        noise=cm.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        if(noise == null)
            Debug.LogError("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {noise}");
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
        DOTween.To(() => cm.m_Lens.OrthographicSize, x => cm.m_Lens.OrthographicSize = x, fieldOfView, duration);
    }

    

    public void ChangeFieldOfViewHit(float newFieldOfView, float oldFieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.OrthographicSize, x => cm.m_Lens.OrthographicSize = x, newFieldOfView, duration).OnComplete(()=>{
            DOTween.To(() => cm.m_Lens.OrthographicSize, x => cm.m_Lens.OrthographicSize = x, oldFieldOfView, duration);
        });
    }

    public void ChangeFollow(Transform Ball)
    {
        cm.m_Follow=Ball;
    }
}
