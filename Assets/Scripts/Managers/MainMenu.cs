using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string[] levelNames;
    public string firstLevel;
    public string levelSelect;
    private LevelDoor levelDoor;

    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);
        
        for (int i = 0; i < levelNames.Length; i++)
        {
            PlayerPrefs.SetInt(levelNames[i], 0);
        }
        PlayerPrefs.SetInt("GemAmount", 0);
        PlayerPrefs.SetInt("PlayerLives", 3);
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
