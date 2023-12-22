using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour
{

    public int damageToGive;
    private LevelManager lvlManager;
    public float speed;
    public float yPosAdd;
    public float yPosMult;

    void Start()
    {
       lvlManager = FindObjectOfType<LevelManager>(); 
    }

    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * yPosMult - yPosAdd;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            lvlManager.DamagePlayerBlock(damageToGive);
        }
    }

}
