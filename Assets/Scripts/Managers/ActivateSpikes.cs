using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpikes : MonoBehaviour
{

    public GameObject spike1;
    public GameObject spike2;
    public GameObject spike3;
    public GameObject spike4;

    private void OnTriggerEnter2D(Collider2D other)
    {
        spike1.SetActive(true);
        spike2.SetActive(true);
        spike3.SetActive(true);
        spike4.SetActive(true);
    }
}
