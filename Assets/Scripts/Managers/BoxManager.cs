using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxManager : MonoBehaviour
{

    public TextMeshProUGUI search;
    public GameObject heart; // 1
    public GameObject gem; // 2
    public GameObject gem3Times; // 3
    public GameObject cherry; // 4
    public Transform spawnLocation;
    private int use;

    private void Start()
    {
        use = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" &&  use == 0)
        {
            use += 1;
            search.gameObject.SetActive(true);
            float randomNumber = Random.Range(1, 5);
            if(randomNumber == 1)
            {
                Instantiate(heart, spawnLocation.position, spawnLocation.rotation);
            } 
            else if (randomNumber == 2)
            {
                Instantiate(gem, spawnLocation.position, spawnLocation.rotation);
            } 
            else if (randomNumber == 3)
            {
                Instantiate(gem3Times, spawnLocation.position, spawnLocation.rotation);
            } 
            else if(randomNumber == 4)
            {
                Instantiate(cherry, spawnLocation.position, spawnLocation.rotation);
            } else if(randomNumber == 5)
            {
                search.text = "ERROR NOTHING";
            }       
        }
    }

}
