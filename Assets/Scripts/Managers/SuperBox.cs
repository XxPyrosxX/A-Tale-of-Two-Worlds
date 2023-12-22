using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuperBox : MonoBehaviour
{

    public TextMeshProUGUI powerUpText;
    private int use;
    private PlayerController player;

    void Start()
    {
        use = 0;
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && use == 0)
        {
            use += 1;
            float randomNumber = Random.Range(1, 2);
            if(randomNumber == 1)
            {
                player.moveSpeed += 5;
                powerUpText.text = "SUPER SPEED";
                powerUpText.gameObject.SetActive(true);
            } else if(randomNumber == 2)
            {
                player.jumpHeight += 5;
                powerUpText.text = "SUPER JUMP";
                powerUpText.gameObject.SetActive(true);
            }
        }
    }
}
