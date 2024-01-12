using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Player : MonoBehaviour
{
    
    [SerializeField] private GameObject explodeParticle;

    [SerializeField] private List<ParticleSystem> buffParticles;
    
    public BallData ballData;
    public GameData gameData;
    
    public int ID;

    internal bool isInTheButton=false;

    internal Rigidbody tempRigidbody;

    internal Rigidbody ballsRigidbody;

    [SerializeField] private GameObject scoreXP;

    public GameObject selectedParticle;


    public bool isOrderMe=false;

    private Camera mainCamera;
    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTrapHitPlayer,OnTrapHitPlayer);
        EventManager.AddIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.AddHandler(GameEvent.OnInvulnerable,OnInvulnerable);
        EventManager.AddHandler(GameEvent.OnVulnerable,OnVulnerable);
        EventManager.AddHandler(GameEvent.OnDestroyer,OnDestroyer);
        EventManager.AddHandler(GameEvent.OnNormal,OnNormal);
        EventManager.AddHandler(GameEvent.OnSpeedUp,OnSpeedUp);
        EventManager.AddHandler(GameEvent.OnSpeedNormal,OnSpeedNormal);

    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTrapHitPlayer,OnTrapHitPlayer);
        EventManager.RemoveIdHandler(GameEvent.OnHitWall,OnHitWall);
        EventManager.RemoveHandler(GameEvent.OnInvulnerable,OnInvulnerable);
        EventManager.RemoveHandler(GameEvent.OnVulnerable,OnVulnerable);
        EventManager.RemoveHandler(GameEvent.OnDestroyer,OnDestroyer);
        EventManager.RemoveHandler(GameEvent.OnNormal,OnNormal);
        EventManager.RemoveHandler(GameEvent.OnSpeedUp,OnSpeedUp);
        EventManager.RemoveHandler(GameEvent.OnSpeedNormal,OnSpeedNormal);

    }

    private void Start() 
    {
        ballsRigidbody=GetComponent<Rigidbody>();
        mainCamera=Camera.main;
        
    }
    private void OnTrapHitPlayer()
    {
        //Cesitlendirilebilir
        //Instantiate(explodeParticle,transform.position,Quaternion.identity);
        if(ballData.isInvulnerable || ballData.isDestroyer)
        {
            Debug.Log("NO DAMAGE");
        }
        else
        {
            EventManager.Broadcast(GameEvent.OnDamagePlayer);
            Debug.Log("DAMAGE TO PLAYER");
            transform.DOScale(new Vector3(transform.localScale.x*2f,transform.localScale.y*1.2f,transform.localScale.z*1.2f),2f).OnComplete(()=>{
                Instantiate(explodeParticle,transform.position,Quaternion.identity);
                EventManager.Broadcast(GameEvent.OnPlayerDead);
                gameObject.SetActive(false);
            });
        }
    }

    

    

    //Bu methodu birkac defa tekrarliyorum. Daha efektif hale cevir
    internal void XPEffect()
    {
        //BUNU 3UNDE DE YAPIYOR. ONA DIKKAT ETMEK LAZIM. BALL CONTROLLERDEN YAZ

        GameObject XP=Instantiate(scoreXP,transform.position,Quaternion.identity);
        XP.transform.LookAt(mainCamera.transform.position);
        XP.transform.DOLocalJump(XP.transform.localPosition,1,1,1,false);
        XP.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + gameData.increaseScore.ToString();
        XP.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>XP.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(XP,2);
    }
    
    
    private void OnHitWall(int id)
    {
        /*if(ID==id)
        {
            transform.DOScale(Vector3.one/1.3f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
        }*/
            
    }

    internal void SetTempRigidbody()
    {
        tempRigidbody=ballData.currentBallRigidbodyData;
    }

    internal void SetZeroBallsRigidbody()
    {
        ballsRigidbody.velocity=Vector3.zero;
        ballsRigidbody.isKinematic=true;
    }
    
    
    

   

    #region Buff
    private void OnInvulnerable()
    {
        Debug.Log("IAM INVINCIBLE");
        ballData.isInvulnerable=true;
        
        OpenParticle(0);
    }
    private void OnVulnerable()
    {
        Debug.Log("OH NO ITS RED SUN");
        ballData.isInvulnerable=false;
        CloseParticles();
    }
    
    private void OnSpeedUp()
    {
        Debug.Log("IAM THE FLASH");
        //WILL IMPROVE
        ballData.BallSpeed=25;
        OpenParticle(1);
    }

    private void OnSpeedNormal()
    {
        Debug.Log("IAM BARRY ALLEN");
        ballData.BallSpeed=20;
        CloseParticles();
    }

    private void OnDestroyer()
    {
        Debug.Log("SUPERMAN");
        ballData.isDestroyer=true;
        OpenParticle(2);
    }
    private void OnNormal()
    {
        Debug.Log("Clark Kent");
        ballData.isDestroyer=false;
        CloseParticles();
    }

    private void OpenParticle(int id)
    {
        buffParticles[id].gameObject.SetActive(true);
        buffParticles[id].Play();
    }
    private void CloseParticles()
    {
        for (int i = 0; i < buffParticles.Count; i++)
        {
            buffParticles[i].Stop();
            buffParticles[i].gameObject.SetActive(false);
        }
    }
    


    #endregion

    
}
