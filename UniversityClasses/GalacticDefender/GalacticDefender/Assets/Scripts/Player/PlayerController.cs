using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    [SerializeField]
    float maxspeedH;        //max player's horizontal speed 
    [SerializeField]
    float maxspeedV;        //max player's verical speed
    [SerializeField]
    float rotAngleH;        //angle of horizontal rotation
    [SerializeField]
    float rotAngleV;        //angle of vertical rotation
    
    //references
    Rigidbody playerRb;     //reference to player's rigidbody

    //shooting variables
    [SerializeField]
    GameObject bullet;      //object that will be spawned
    [SerializeField]
    Transform gunTip;       //place where will be bullet spawned
    float fireRate;         //players fire rate
    float fireTime;         //time when the next shot will be

    //input variables
    float moveHor;          //variable to get input for horizontal movement
    float moveVer;          //variable to get input for vertical movement
    float rotHor;           //variable to get horizontal rotation
    float rotVer;           //variable to get vertical rotation
    Quaternion rotationLimits = Quaternion.Euler(new Vector3(15f, 15f, 0f)); //limits of ratation

    void Awake() {
        //initializing variables
        fireRate = 0.5f;
        fireTime = 0f;
        //getting rigidbody reference
        playerRb = GetComponent<Rigidbody>(); 
    }

    void Update() {
        //firing bullet on input
        if(Input.GetKey("space")) {
            Fire();
        }
        //getting player's input
        moveHor = Input.GetAxis("Horizontal");
        moveVer = Input.GetAxis("Vertical");
        rotHor = Input.GetAxis("HorizontalRotate");
        rotVer = Input.GetAxis("VerticalRotate");
    }

    void FixedUpdate() {
        //moving player in right direction
        playerRb.AddForce(new Vector3(moveHor*maxspeedH, moveVer*maxspeedV, 0.0f)); 
    
        //rotating ship
        playerRb.rotation = Quaternion.RotateTowards(playerRb.rotation, Quaternion.Euler(playerRb.rotation.eulerAngles.x, playerRb.rotation.eulerAngles.y, moveHor*rotAngleH), 1.0f);
        playerRb.rotation *= Quaternion.Euler(rotVer*rotAngleV/100f, -rotHor*rotAngleH/100f, 0.0f);
    
        //checking rotation limits
        if(playerRb.rotation.eulerAngles.y < 345f && playerRb.rotation.eulerAngles.y > 300f)
            playerRb.rotation = Quaternion.Euler(playerRb.rotation.eulerAngles.x, -rotationLimits.eulerAngles.y, playerRb.rotation.eulerAngles.z);
        else if(playerRb.rotation.eulerAngles.y > rotationLimits.eulerAngles.y && playerRb.rotation.eulerAngles.y < 100f)
            playerRb.rotation = Quaternion.Euler(playerRb.rotation.eulerAngles.x, rotationLimits.eulerAngles.y, playerRb.rotation.eulerAngles.z);
        
        if(playerRb.rotation.eulerAngles.x < 345f && playerRb.rotation.eulerAngles.x > 300f)
            playerRb.rotation = Quaternion.Euler(-rotationLimits.eulerAngles.x, playerRb.rotation.eulerAngles.y, playerRb.rotation.eulerAngles.z);
        else if(playerRb.rotation.eulerAngles.x > rotationLimits.eulerAngles.x && playerRb.rotation.eulerAngles.x < 100f)
            playerRb.rotation = Quaternion.Euler(rotationLimits.eulerAngles.x, playerRb.rotation.eulerAngles.y, playerRb.rotation.eulerAngles.z);
        
        //borders for player movement
        if(Mathf.Abs(transform.position.x) >= 24)
            transform.position = new Vector3(24 * Mathf.Sign(transform.position.x), transform.position.y, 0f);
        if(Mathf.Abs(transform.position.y) >= 13)
            transform.position = new Vector3(transform.position.x, 13 * Mathf.Sign(transform.position.y), 0f);
    }

    //function firing bullet
    void Fire() {
        //if next shot is available
        if(Time.time > fireTime) {
            //setting next shot time
            fireTime = Time.time + fireRate;
            //spawning bullet
            Instantiate(bullet, gunTip.position, gunTip.rotation);
        }
    }
}
