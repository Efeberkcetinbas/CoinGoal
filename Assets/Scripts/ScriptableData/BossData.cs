using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "BossData", menuName = "Data/BossData", order = 4)]
public class BossData : ScriptableObject {
    public string Name;
    
    public int Health;
    public int TempHealth;
    public int ReqPass;


}