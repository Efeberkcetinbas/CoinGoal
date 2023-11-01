using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    [SerializeField] private List<Image> stars=new List<Image>();
    public GameData gameData;
}
