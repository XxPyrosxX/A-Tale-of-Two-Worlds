using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnPlayerDeath : MonoBehaviour
{

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;

    private Rigidbody2D myRB;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;
        if(GetComponent<Rigidbody2D>() != null)
        {
            myRB= GetComponent<Rigidbody2D>();
        }
    }

    public void ResetAfterDeath()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;

        if(myRB != null)
        {
            myRB.velocity = Vector3.zero;
        }
    }

}
