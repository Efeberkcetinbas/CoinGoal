using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;

    private int randomIndex;
    public ParticleSystem particle;
    public ParticleSystem leaveParticle;

    [SerializeField] private List<Sprite> emojis=new List<Sprite>();
    private SpriteRenderer spriteRenderer;

    public PlayerData playerData;
    private void Start() 
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

   
   

   

    private void OnSaw()
    {
        spriteRenderer.color=Color.red;
        StartCoroutine(GetBackColor());
    }

    private IEnumerator GetBackColor()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color=Color.white;
    }
}
