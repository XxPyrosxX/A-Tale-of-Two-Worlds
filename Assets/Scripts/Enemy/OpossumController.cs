using UnityEngine;

public class OpossumController : MonoBehaviour
{

    public float moveSpeed;

    private Rigidbody2D myRB;
    
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0f);
    }

}
