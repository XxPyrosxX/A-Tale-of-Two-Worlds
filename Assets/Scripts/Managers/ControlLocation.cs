using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLocation : MonoBehaviour
{

    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (player.transform.position.x < -8.87 || player.transform.position.x > 7.54)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
}
