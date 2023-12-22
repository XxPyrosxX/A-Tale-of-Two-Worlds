using UnityEngine;

public class DestroyAfterEnd : MonoBehaviour
{

    public float lifetime;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        } 
    }
}
