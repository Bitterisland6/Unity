using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    [SerializeField]
    float maxspeedH;
    [SerializeField]
    float maxspeedV;
    [SerializeField]
    float rotAngleH;
    [SerializeField]
    float rotAngleV;
    
    //references
    Rigidbody playerRb;

    //shooting variables
    [SerializeField]
    GameObject bullet;      //object that will be spawned
    [SerializeField]
    Transform gunTip;       //place where will be bullet spawned
    float fireRate = 0.5f;  //player's fire rate
    float fireTime = 0f;    //time when the next shot will be

    //input variables
    float moveHor;
    float moveVer;
    float rotHor;
    float rotVer;
    Quaternion rotationLimits = Quaternion.Euler(new Vector3(15f, 15f, 0f));

    void Start() {
        //getting player rigidbody
        playerRb = GetComponent<Rigidbody>(); 
    }

    void Update() {
        //firing rocket
        if(Input.GetKey("space")) {
            Fire();
        }
        //getting input form player
        moveHor = Input.GetAxis("Horizontal");
        moveVer = Input.GetAxis("Vertical");
        rotHor = Input.GetAxis("HorizontalRotate");
        rotVer = Input.GetAxis("VerticalRotate");
    }

    void FixedUpdate() {
        //moving part
        playerRb.AddForce(new Vector3(moveHor*maxspeedH, moveVer*maxspeedV, 0.0f)); 
    
        //rotating ship
        playerRb.rotation = Quaternion.RotateTowards(playerRb.rotation, Quaternion.Euler(playerRb.rotation.eulerAngles.x, playerRb.rotation.eulerAngles.y, moveHor*rotAngleH), 1.0f);
        playerRb.rotation *= Quaternion.Euler(rotVer*rotAngleV/100f, -rotHor*rotAngleH/100f, 0.0f);
    
             if(playerRb.rotation.eulerAngles.y < 345f && playerRb.rotation.eulerAngles.y > 300f)
                playerRb.rotation               = Quaternion.Euler(playerRb.rotation.eulerAngles.x, -rotationLimits.eulerAngles.y, playerRb.rotation.eulerAngles.z);
        else if(playerRb.rotation.eulerAngles.y > rotationLimits.eulerAngles.y && playerRb.rotation.eulerAngles.y < 100f)
                playerRb.rotation               = Quaternion.Euler(playerRb.rotation.eulerAngles.x,  rotationLimits.eulerAngles.y, playerRb.rotation.eulerAngles.z);
        
             if(playerRb.rotation.eulerAngles.x < 345f && playerRb.rotation.eulerAngles.x > 300f)
                playerRb.rotation               = Quaternion.Euler(-rotationLimits.eulerAngles.x, playerRb.rotation.eulerAngles.y, playerRb.rotation.eulerAngles.z);
        else if(playerRb.rotation.eulerAngles.x > rotationLimits.eulerAngles.x && playerRb.rotation.eulerAngles.x < 100f)
                playerRb.rotation               = Quaternion.Euler(rotationLimits.eulerAngles.x, playerRb.rotation.eulerAngles.y, playerRb.rotation.eulerAngles.z);
        
        //putting player on other side of the map
        if(Mathf.Abs(transform.position.x) >= 24)
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0f);
        if(Mathf.Abs(transform.position.y) >= 13)
            transform.position = new Vector3(transform.position.x, -transform.position.y, 0f);
    }

    void Fire(){
        if(Time.time > fireTime) {
            fireTime = Time.time + fireRate;
            Instantiate(bullet, gunTip.position, gunTip.rotation);
        }
    }
}
