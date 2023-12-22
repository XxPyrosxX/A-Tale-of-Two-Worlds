using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseControl : MonoBehaviour
{

    public GameObject doorClosed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            doorClosed.SetActive(false);
        }
    }
}
