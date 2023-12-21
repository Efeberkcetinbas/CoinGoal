using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxGenerator : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    //Dotween ile guzel bir background olussun
    //[SerializeField] private List<GameObject> backgroundGameObjects=new List<GameObject>();
    [SerializeField] private List<Material> skyboxMaterials=new List<Material>();
    //[SerializeField] private List<Color> fogColors=new List<Color>();

    

    private void Start() 
    {
        Check();
    }

    private void Check()
    {
        RenderSettings.skybox=skyboxMaterials[gameData.skyboxIndex];
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        
    }

    private void OnNextLevel()
    {
        if(gameData.LevelNumberIndex>0 && gameData.LevelNumberIndex % 5 == 0)
        {
            //Generate();
            ChangeSkybox();
        }
    }



    private void ChangeSkybox()
    {
        gameData.skyboxIndex++;

        //gameData.fogColorIndex++;
        gameData.skyboxIndex=gameData.skyboxIndex % skyboxMaterials.Count;
        RenderSettings.skybox=skyboxMaterials[gameData.skyboxIndex];

    }
}
