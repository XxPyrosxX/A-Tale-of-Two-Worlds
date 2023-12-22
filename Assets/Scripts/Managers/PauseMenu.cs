using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseScreenButton;
    private PlayerController player;
    public string levelSelect;
    public string mainMenu;
    public GameObject pauseScreen;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        player.canMove = true;
        levelManager.levelMusic.UnPause();
    }

    public void LevelSelectLoad()
    {
        PlayerPrefs.SetInt("GemAmount", levelManager.gemAmount);
        PlayerPrefs.SetInt("PlayerLives", levelManager.currentLivesCount);
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSelect);
    }

    public void MainMenuLoad()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }

    public void PauseGame()
    {
        EventSystem.current.SetSelectedGameObject(pauseScreenButton);
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        player.canMove = false;
        levelManager.levelMusic.Pause();
    }
}
