using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool respawnActive;
    public GameObject gameOverScreen;
    public float respawnCountdown;
    public GameObject deathExplosion;
    public int gemAmount;
    public TextMeshProUGUI gemText;
    public float maxHealth;
    public float currentHealth;
    public bool invincibilityFrames;

    public AudioSource gemSound;
    public AudioSource levelMusic;
    public AudioSource gameOverMusic;
    public AudioSource victoryMusic;
    public AudioSource endOfLevelMusic;

    public int bonusLifeThreshold;
    public int gemExtraLifeCounter;
    public int startLivesCount;
    public int currentLivesCount;
    public TextMeshProUGUI livesText;
    public Image livesImage;
    public Image healthBarImage;
    public Sprite standing;
    public Sprite crouch;
    public Sprite facingDeath;

    private PlayerController player;
    private GameObject killPlane;
    private bool canRespawn;
    private HealthBar healthBar;
    private SpriteRenderer mySR;
    private List<ResetOnPlayerDeath> objectsResetting;
    private ActivateSpikes AS;
    public int numberOfFlahes;


    void Start()
    {
        if(PlayerPrefs.HasKey("GemAmount"))
        {
            gemAmount = PlayerPrefs.GetInt("GemAmount");
        }
        else
        {
            gemAmount= 0;
        }

        if(PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLivesCount = PlayerPrefs.GetInt("PlayerLives");
        } 
        else
        {
            currentLivesCount = startLivesCount;
        }
        player = FindObjectOfType<PlayerController>();
        killPlane = GameObject.FindGameObjectWithTag("KillPlane");
        gemAmount= 0;
        gemText.text = "Gems: " + gemAmount;

        currentHealth = maxHealth;
        healthBar = GameObject.FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        canRespawn = true;

        currentLivesCount = startLivesCount;
        livesText.text = "x: " + currentLivesCount;

        objectsResetting = FindObjectsOfType<ResetOnPlayerDeath>().ToList();
        AS = FindObjectOfType<ActivateSpikes>();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {

            Respawn();
        }

        if(gemExtraLifeCounter >= bonusLifeThreshold)
        {
            currentLivesCount++;
            livesText.text = "x: " + currentLivesCount;
            gemExtraLifeCounter -= bonusLifeThreshold;
        }

        if (currentLivesCount >= 3)
        {
            livesImage.sprite = standing;
        }
        else if (currentLivesCount == 2)
        {
            livesImage.sprite = crouch;
        }
        else if (currentLivesCount == 1)
        {
            livesImage.sprite = facingDeath;
        }

        if(currentHealth >= 51)
        {
            healthBarImage.sprite = standing;
        } else if (currentHealth < 51 && currentHealth >= 26)
        {
            healthBarImage.sprite = crouch;
        }
     else if (currentHealth < 26)
        {
            healthBarImage.sprite = facingDeath;
        }
    }

    public void Respawn()
    {
        if (canRespawn)
        {
            currentLivesCount -= 1;
            livesText.text = "x: " + currentLivesCount;

            if (currentLivesCount > 0)
            {
                canRespawn = false;
                StartCoroutine("RespawnCoroutine");
            } else
            {
                player.gameObject.SetActive(false);
                livesText.text = "x: 0";
                gameOverScreen.SetActive(true);
                levelMusic.Stop();
                gameOverMusic.Play();
            }   
        }
        AS.spike1.SetActive(false);
        AS.spike2.SetActive(false);
        AS.spike3.SetActive(false);
        AS.spike4.SetActive(false);
    }

    public IEnumerator RespawnCoroutine()
    {
        respawnActive = true;

        player.gameObject.SetActive(false);

        killPlane.SetActive(false);

        Instantiate(deathExplosion, player.transform.position, player.transform.rotation);

        yield return new WaitForSeconds(respawnCountdown);

        respawnActive = false;

        currentHealth = maxHealth;
        canRespawn= true;
        healthBar.SetCurrentHealth(currentHealth/maxHealth);

        player.transform.position = player.respawnPos;

        foreach(ResetOnPlayerDeath obj in objectsResetting)
        {
            obj.gameObject.SetActive(true);
            obj.ResetAfterDeath();
        }

        gemAmount= 0;
        gemExtraLifeCounter= 0;
        gemText.text = "Gems: " + gemAmount;

        player.gameObject.SetActive(true);

        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);   

        yield return new WaitForSeconds(1);

        killPlane.SetActive(true);
    }

    public IEnumerator IFramesFlash()
    {
        for(int i = 0; i < numberOfFlahes; i++)
        {
            player.mySR.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            player.mySR.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void AddGems(int amountOfGems)
    {   
        gemAmount += amountOfGems;
        gemExtraLifeCounter += gemAmount;
        gemText.text = "Gems: " + gemAmount;
        gemSound.Play();
    }

    public void DamagePlayer(float dmg)
    {
        if(!invincibilityFrames)
        {
            currentHealth -= dmg;
            healthBar.SetCurrentHealth(currentHealth / maxHealth);
            player.hurtSound.Play();
            if(currentHealth > 0)
            {
                player.Knockback();
                StartCoroutine("IFramesFlash");
            }
        }
    }
    
    public void DamagePlayerBlock(float dmg)
    {
        if(!invincibilityFrames)
        {
            currentHealth -= dmg;
            healthBar.SetCurrentHealth(currentHealth / maxHealth);
            player.hurtSound.Play();
            if(currentHealth > 0)
            {
                StartCoroutine("IFramesFlash");
            }
        }
    }

    public void RegenHealth(float amt) 
    {
        currentHealth += amt;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetCurrentHealth(currentHealth/maxHealth);
    }

    public void AddLife(int lives) 
    {
        currentLivesCount += lives;
        livesText.text = "x: " + currentLivesCount;
        gemSound.Play();
    }

    public void GiveHealth(int healthToGive)
    {
        currentHealth += healthToGive;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetCurrentHealth(currentHealth/maxHealth);
    }

}
