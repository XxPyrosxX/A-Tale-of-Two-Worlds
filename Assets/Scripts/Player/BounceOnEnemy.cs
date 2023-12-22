using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnEnemy : MonoBehaviour
{
    public GameObject deathAnim;
    public float bounceForce;
    private Rigidbody2D playerRB;

    void Start()
    {
        playerRB = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            Instantiate(deathAnim, other.transform.position, other.transform.rotation);
            playerRB.velocity = new Vector3(playerRB.velocity.x,bounceForce,0f);
        }   
        if(other.tag == "Boss")
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, 0f);
            other.transform.parent.GetComponent<Boss>().takeDamage = true;
        }
    }
}
