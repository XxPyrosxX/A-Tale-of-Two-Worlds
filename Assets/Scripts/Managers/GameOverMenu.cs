using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public string levelSelect;
    public string mainMenu;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RestartLevel()
    {
        PlayerPrefs.SetInt("GemAmount", 0);
        PlayerPrefs.SetInt("PlayerLives", levelManager.startLivesCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelectLoad()
    {
        PlayerPrefs.SetInt("GemAmount", 0);
        PlayerPrefs.SetInt("PlayerLives", levelManager.startLivesCount);
        SceneManager.LoadScene(levelSelect);
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene(mainMenu);
    }
    
}
