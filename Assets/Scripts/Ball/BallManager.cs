using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BallManager : MonoBehaviour
{
    public List<GameObject> balls=new List<GameObject>();

    public int index;

    public Color activeColor,deActiveColor;
    

    private void Start()
    {
        OpenSignal();
    }
    
    public void OpenSignal()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].transform.GetChild(0).transform.localScale=Vector3.zero;
            
            balls[i].transform.GetComponent<SpriteRenderer>().color=deActiveColor;
            balls[i].GetComponent<BallControl>().canShoot=false;    
        }
        
        balls[index].transform.GetChild(0).gameObject.SetActive(true);
        balls[index].transform.GetChild(0).transform.DOScale(Vector3.one*1.5f,0.5f);
        balls[index].transform.GetComponent<SpriteRenderer>().color=activeColor;
        balls[index].GetComponent<BallControl>().canShoot=true;    
        //GameManager.Instance.LineOpenControl(index);
    }

    public void IncreaseIndex()
    {
        balls[index].GetComponent<BallControl>().canShoot=false;
        index++;
    }

    public void CheckIndex()
    {
        if (index == balls.Count) index = 0;
    }

    public IEnumerator ResetBallSpeed(float dragPower)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            yield return new WaitForSeconds(1f);
            balls[i].GetComponent<Rigidbody2D>().drag=dragPower;
        }
    }

    public void StopBall()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].GetComponent<Rigidbody2D>().drag=150;
        }
    }
    
}
