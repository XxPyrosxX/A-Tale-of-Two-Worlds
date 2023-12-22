using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour 
{
    
    public Sprite doorLocked;
    public Sprite doorUnlocked;

    public SpriteRenderer mySR;

    public string levelToLoad;
    public bool isUnlocked;

    private void Start()
    {
        PlayerPrefs.SetInt("Level1", 1);
        mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = doorLocked;
        if(PlayerPrefs.GetInt(levelToLoad) == 1)
        {
            isUnlocked = true;
        }
        else
        {
            isUnlocked = false;
        }

        if(isUnlocked)
        {
            mySR.sprite = doorUnlocked;
        } 
        else
        {
            mySR.sprite = doorLocked;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetButton("Jump") && isUnlocked)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}
