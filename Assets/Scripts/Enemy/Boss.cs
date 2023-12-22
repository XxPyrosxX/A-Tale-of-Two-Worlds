using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool waitingForRespawn;
    private LevelManager levelManager;
    private float storedSawDropTime;
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform sawSpawnPoint;
    public GameObject spinSaw;
    public GameObject boss;
    public bool bossRight;
    public GameObject victoryPlatform;
    public GameObject rightPlatforms;
    public GameObject leftPlatforms;
    public bool takeDamage;
    public int bossHealth;
    private int currentBossHealth;
    private CameraController theCamera;
    public bool battleActive;
    public float timeBetweenSaws;
    public float timeUntilPlatforms;
    private float sawDropTimer;
    private float platformAppearTimer;

    void Start()
    {
        sawDropTimer = timeBetweenSaws;
        platformAppearTimer = timeUntilPlatforms;
        boss.transform.position = rightPoint.position;
        bossRight = true;
        currentBossHealth = bossHealth;
        theCamera= FindObjectOfType<CameraController>();
        levelManager = FindObjectOfType<LevelManager>();
        storedSawDropTime = sawDropTimer;
    }

    void Update()
    {
        if(levelManager.respawnActive)
        {
            battleActive= false;
            waitingForRespawn=true;
        }
        if(waitingForRespawn && !levelManager.respawnActive)
        {
            boss.SetActive(false);
            rightPlatforms.SetActive(false);
            leftPlatforms.SetActive(false);
            platformAppearTimer = timeUntilPlatforms;
            sawDropTimer = storedSawDropTime;
            boss.transform.position = rightPoint.position;
            bossRight = true;
            currentBossHealth = bossHealth;
            theCamera.following = true;
            waitingForRespawn = false;
        }
        if(battleActive)
        {
            theCamera.following = false;
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(transform.position.x + 15, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.slideInTime * Time.deltaTime);
            
            boss.SetActive(true);

            if(sawDropTimer > 0f)
            {
                sawDropTimer -= Time.deltaTime;
            }
            else
            {
                sawSpawnPoint.position = new Vector3(Random.Range(leftPoint.position.x, rightPoint.position.x), sawSpawnPoint.position.y, sawSpawnPoint.position.z);
                Instantiate(spinSaw, sawSpawnPoint.position, sawSpawnPoint.rotation);
                sawDropTimer = timeBetweenSaws;
            }
            if(bossRight)
            {
                if(platformAppearTimer > 0f)
                {
                    platformAppearTimer -= Time.deltaTime;
                }
                else
                {
                    rightPlatforms.SetActive(true);
                }
            }
            else
            {
                if (platformAppearTimer > 0f)
                {
                    platformAppearTimer -= Time.deltaTime;
                }
                else
                {
                    leftPlatforms.SetActive(true);
                }
            }
            if(takeDamage)
            {
                currentBossHealth -= 1;
                if(currentBossHealth <= 0)
                {
                    theCamera.following = true;
                    victoryPlatform.SetActive(true);
                    gameObject.SetActive(false);
                }

                if(bossRight)
                {
                    boss.transform.position = leftPoint.position;
                }
                else
                {
                    boss.transform.position = rightPoint.position;
                }
                bossRight = !bossRight;
                rightPlatforms.SetActive(false);
                leftPlatforms.SetActive(false);
                platformAppearTimer = timeUntilPlatforms;
                timeBetweenSaws /= 2f;
                takeDamage = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            battleActive= true;
        }
    }
}
