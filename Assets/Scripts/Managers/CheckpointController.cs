using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    public Sprite crankUp;
    public Sprite crankDonw;
    public bool isCheckpointActivated;

    private SpriteRenderer mySpriteRenderer;
    
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            mySpriteRenderer.sprite = crankUp;
            isCheckpointActivated = true;
        }
    }

}
