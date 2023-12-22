using System.Collections;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    public int damageToGive;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            levelManager.DamagePlayer(damageToGive);
        }
    }
}
