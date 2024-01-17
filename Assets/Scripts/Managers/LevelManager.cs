using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{

    [Header("Indexes")]
    public int levelIndex;
    
    public GameData gameData;
    public List<GameObject> levels;

    private MiniGameControl miniGameControl;

    [Header("Level System")]
    [SerializeField] private List<LevelButton> levelButtons=new List<LevelButton>();
    [SerializeField] private bool isCompletedAllLevels=false;


    private void Awake() 
    {
        gameData.LoadData();
        LoadLevel();
    }
    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnLoadNextLevel,LoadNextLevel);
        EventManager.AddHandler(GameEvent.OnOpenLevelSystem,OnOpenLevelSystem);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnLoadNextLevel,LoadNextLevel);
        EventManager.RemoveHandler(GameEvent.OnOpenLevelSystem,OnOpenLevelSystem);

    }
    private void LoadLevel()
    {


        levelIndex = PlayerPrefs.GetInt("NumberOfLevel");
        
        if (levelIndex == levels.Count)
        {
            levelIndex = 0;
            isCompletedAllLevels=true;
        } 

        PlayerPrefs.SetInt("NumberOfLevel", levelIndex);
       

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
        levels[levelIndex].SetActive(true);
    }

    private void LoadNextLevel()
    {
        PlayerPrefs.SetInt("NumberOfLevel", levelIndex + 1);
        PlayerPrefs.SetInt("RealNumberLevel", PlayerPrefs.GetInt("RealNumberLevel", 0) + 1);
        gameData.LevelNumberIndex++;
        LoadLevel();
        EventManager.Broadcast(GameEvent.OnNextLevel);
        gameData.SaveData();
        miniGameControl=FindObjectOfType<MiniGameControl>();
        if(miniGameControl.isMiniGame)
        {        
            EventManager.Broadcast(GameEvent.OnMiniGameActive);
            EventManager.Broadcast(GameEvent.OnMiniGameBall);
        }
    }
    
    public void RestartLevel()
    {
        LoadLevel();
        EventManager.Broadcast(GameEvent.OnRestartLevel);
    }

    private void OnApplicationQuit() 
    {
        gameData.SaveData();
    }


    #region LevelSystem

    private void OnOpenLevelSystem()
    {
        if(!isCompletedAllLevels)
        {
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].levelButton.interactable=false;
                levelButtons[i].lockImage.SetActive(true);
                levelButtons[i].levelText.SetText((i+1).ToString());
            }
            for (int i = 0; i < levelIndex; i++)
            {
                levelButtons[i].levelButton.interactable=true;
                levelButtons[i].lockImage.SetActive(false);
            }
        }
        
    }

    #endregion
    
}
