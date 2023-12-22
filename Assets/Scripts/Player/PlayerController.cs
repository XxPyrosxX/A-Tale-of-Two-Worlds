using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //want to control speed for player 
    //want access to our own rigid body component
    public float moveSpeed;
    public float jumpHeight;
    public Transform groundCheckSpot;
    public float groundCheckRadius;
    public LayerMask whatLayerIsGround;
    public bool isGrounded;
    public bool isCrouched;
    public bool isClimbing;
    public bool onLadder;
    public bool canMove;

    public float iFrames;
    private float counteriFrames;
    public float knockbackForce;
    public float knockbackFrames;
    private float knockbackCounter;
    public GameObject bounceBox;
    public AudioSource jumpSound;
    public AudioSource hurtSound;

    private LevelManager levelManager;
    private float intialGravity;
    public Vector3 respawnPos;
    public Rigidbody2D myRB;
    private Animator myAnimator;
    private Transform prevParent;
    public SpriteRenderer mySR;

    
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        intialGravity = myRB.gravityScale;
        levelManager = FindObjectOfType<LevelManager>();
        mySR = GetComponent<SpriteRenderer>();
        canMove = true;
    }

    
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheckSpot.position, groundCheckRadius, whatLayerIsGround);

        if(knockbackCounter <= 0f && canMove)
        {
            //GetKey will return true as long as key is pressed down
            //KeyCode allows us to grab any key without knowing special code
            if (Input.GetAxisRaw("Horizontal") > 0f && canMove)
            {
                //velocity is how quick the player is moving
                //Vector3 has x, y, and z values
                // x - movespeed, y - our current y, z - never change since we are 2D
                myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0f);

                transform.localScale = new Vector3(1f, 1f, 1f);

            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRB.velocity = new Vector3(-moveSpeed, myRB.velocity.y, 0f);

                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                myRB.velocity = new Vector3(0f, myRB.velocity.y, 0f);
            }

            if (Input.GetButtonDown("Jump") && isGrounded && !onLadder)
            {
                myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0f);
                jumpSound.Play();
            }

            if (onLadder)
            {
                isClimbing = true;
                if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    myRB.velocity = new Vector3(myRB.velocity.x, -moveSpeed, 0f);
                }
                else if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    myRB.velocity = new Vector3(myRB.velocity.x, moveSpeed, 0f);
                }
                else
                {
                    myRB.velocity = new Vector3(0f, 0f, 0f);
                }

                if (myRB.velocity.y == 0 && isGrounded)
                {
                    isClimbing = false;
                    onLadder = false;
                }
            }

            if (!onLadder)
            {
                isClimbing = false;
                if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    isCrouched = true;
                }
                else if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    isCrouched = false;
                }
            }
        }

        if(knockbackCounter > 0f)
        {
            knockbackCounter -= Time.deltaTime;

            if(transform.localScale.x > 0f)
            {
                myRB.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            } else
            {
                myRB.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
        }

        if(counteriFrames > 0f)
        {
            counteriFrames -= Time.deltaTime;
        }

        if(counteriFrames <= 0)
        {
            levelManager.invincibilityFrames = false;
        }

        myAnimator.SetFloat("speed", Mathf.Abs(myRB.velocity.x));
        myAnimator.SetFloat("height", myRB.velocity.y);
        myAnimator.SetBool("grounded", isGrounded);
        myAnimator.SetBool("crouched", isCrouched);
        myAnimator.SetBool("climbing", isClimbing);
        if (myRB.velocity.y < 0f)
        {
            bounceBox.SetActive(true);
        }
        else
        {
            bounceBox.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "KillPlane")
        {
            levelManager.Respawn();
        }
        if(other.tag == "Checkpoint")
        {
            respawnPos = other.transform.position;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Ladder" && Input.GetButton("Vertical"))
        {
            myRB.gravityScale = 0;
            onLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Ladder")
        {
            myRB.gravityScale = intialGravity; 
            onLadder = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    { 
        if(other.gameObject.tag == "MovingPlatform" || other.gameObject.tag == "MovingPlatformV")
        {
            prevParent = transform.parent;
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "MovingPlatform" || other.gameObject.tag == "MovingPlatformV")
        {
            transform.parent = prevParent;
        }
    }

    public void Knockback()
    {
        knockbackCounter = knockbackFrames;

        counteriFrames = iFrames;

        levelManager.invincibilityFrames = true;
    }
}
