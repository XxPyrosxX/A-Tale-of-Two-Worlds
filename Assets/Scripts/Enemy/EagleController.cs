using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{

    public Transform leftPatrolPoint;
    public Transform rightPatrolPoint;
    public float moveSpeed;
    public bool movingRight;

    private Rigidbody2D myRB;

    void Start()
    {
        myRB= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(movingRight && transform.position.x > rightPatrolPoint.position.x)
        {
            movingRight= false;
        }

        if(!movingRight && transform.position.x < leftPatrolPoint.position.x)
        {
            movingRight= true;
        }

        if(movingRight)
        {
            myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else
        {
            myRB.velocity = new Vector3(-moveSpeed, myRB.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
