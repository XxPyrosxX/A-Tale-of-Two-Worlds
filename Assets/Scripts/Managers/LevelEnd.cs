using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string levelNameToLoad;
    public float whenToMove;
    public float whenToLoad;
    public bool movePlayer;
    public string levelNameToUnlock;

    private PlayerController player;
    private CameraController theCamera;
    private LevelManager levelManager;
    private bool alreadyPlayed;
    public GameObject openDoor;
           
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        theCamera= FindObjectOfType<CameraController>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            openDoor.SetActive(true);
            StartCoroutine("LevelEndCoroutine");
        }
    }

    public IEnumerator LevelEndCoroutine()
    {
        player.canMove = false;
        theCamera.following = false;
        levelManager.invincibilityFrames = true;
        player.myRB.velocity = Vector3.zero;

        levelManager.levelMusic.Stop();
        
        if(!alreadyPlayed)
        {
            levelManager.victoryMusic.Play();
            alreadyPlayed = true;
        }

        PlayerPrefs.SetInt("GemAmount", levelManager.gemAmount);
        PlayerPrefs.SetInt("PlayerLives", levelManager.currentLivesCount);
        PlayerPrefs.SetInt(levelNameToUnlock, 1);

        yield return new WaitForSeconds(whenToMove);

        movePlayer = true;

        levelManager.victoryMusic.Stop();
        if(!levelManager.endOfLevelMusic.isPlaying)
        {
            levelManager.endOfLevelMusic.Play();
        }
            
        yield return new WaitForSeconds(1f);

        if(SceneManager.GetActiveScene().name == "BossLevel8")
        {
            SceneManager.LoadScene("VictoryScreen");
        }
        else
        {
            SceneManager.LoadScene(levelNameToLoad);
        }

    }

}
