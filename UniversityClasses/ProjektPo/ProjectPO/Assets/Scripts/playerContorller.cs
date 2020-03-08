using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerContorller : MonoBehaviour
{
    //movement variables
    public float maxSpeed; //maximum speed of a player
    
    //jumping variables
    bool grounded = false; //checking if our character is on the ground
    float groundCheckRadius = 0.2f; //the radious of an object wich is used to detect collision with ground
    public LayerMask groundLayer; //layer wich says if object is ground
    public Transform groundCheck; //object used to check if character is on the ground
    public float jumpHeight; //jump height of a player
    private bool canDoubleJump; //checking if player can do double jump
    
    //shooting variables
    public Transform gunTip; //where our projectile is spawned
    public GameObject bullet; //object that will be spawned
    private float fireRate = 1f; //fire rate of player's weapon
    private float nextFire = 0f; //if we can fire already

    //references
    Rigidbody2D myRB; //rigidbody of a player
    private Animator myAnim; //player's animator
    private bool facingRight; //checking if our player is facing right or left

    void Start() {
        //initialization of components
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        
        //at the beggining character is turned right
        facingRight = true;
    }

    void Update() {
        //if player press space key
        if(Input.GetButtonDown("Jump")) {
            //if we are on the ground
            if(grounded) {
                grounded = false;
                //setting right animation and velocity
                myAnim.SetBool("isGrounded", false);
                myRB.AddForce(new Vector2(0, jumpHeight));
                //if we jumped once, we can do doublejump
                canDoubleJump = true;
            } else {
                //doublejump done
                if(canDoubleJump) {
                    canDoubleJump = false;
                    myRB.velocity = new Vector2(myRB.velocity.x, 0);
                    myRB.AddForce(new Vector2(0, jumpHeight*0.8f));
                }
            }
        }

        //player shooting
        if(Input.GetButtonDown("Fire1"))
            fireRocket();
    }

    void FixedUpdate() {
        //jumping part
        //check if we are grounded - if no, then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);
        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);

        //walking part
        //getting input from player, and setting right animation
        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));
        //setting speed based on input
        myRB.velocity = new Vector2(move*maxSpeed,myRB.velocity.y);

        //turning character lef or right
        if( move > 0 && !facingRight)
            flip();
        else if (move < 0 && facingRight)
            flip();
    }

    //function turning character to the oposite site
    void flip() {
        facingRight = !facingRight;
        Vector3 rotate = new Vector3(0f,180f,0f);
        transform.Rotate(rotate, Space.Self);
    }

    //function firing a rocket
    void fireRocket() {
        //if we are able to shoot already
        if(Time.time > nextFire) {
            //delay in next atack
            nextFire = Time.time + fireRate;
            //setting preFab in right rotation
            if(facingRight) //right
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            else //left
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
        }
    }
}
