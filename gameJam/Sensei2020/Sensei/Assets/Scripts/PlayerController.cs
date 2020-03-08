using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //references
    CharacterController chC;                                //character controller on the player
    Camera myCam;                                           //camera attached to player
    Dictionary<string, int> visitedRoom;                    //dictionary saving nuber of times each room was visited by player
    [HideInInspector]
    public Dictionary<string, GameObject> colected;         //array of colected items
    AudioSource myAS;                                       //audio source on player
    
    
    //variables
    [SerializeField]
    float mouseSensitivity = 50f;                           //mouse sensitivity
    [SerializeField]
    float speed = 6f;                                       //player's speed
    [SerializeField]
    Transform playerBody;                                   //player's body
    [SerializeField]
    Transform groundCheck;                                  //player's child gameobject checking collision with ground 
    [SerializeField]
    float groundDistance = 0.2f;                            //maximal distance from the ground where player is grounded
    [SerializeField]
    LayerMask groundMask;                                   //layer of the ground
    [SerializeField]
    public  float jumpForce = 3f;                           //force of players jump
    [SerializeField]
    AudioClip song;                                         //song played durong game
    [SerializeField]
    AudioClip coin;                                         //song played on collecting coin
    [SerializeField]
    AudioClip button;                                       //sound of button pressed

    //private variables
    private float horVel;                                   //input from horizontal axis
    private float verVel;                                   //input from vertical axis
    private float mouseX;                                   //mouse movement in x axis
    private float rotationX;                                //mouse rotation in y axis
    private Vector3 move;                                   //movement of the player
    private Vector3 velocity;                               //players velocity
    private bool isGrounded;                                //flag saying if player is on the ground
    private float gravity;                                  //map's gravity

    //shaking camera variables
    private float shakeDuration;                            //duration of camera shake
    private float shakeAmount;                              //amount of shake apllied to camera
    private float decreaseFactor;                           //factor of stopping camera shake
    private float shakeStart;                               //start of the shake
    private bool shake;                                     //flag saying if camera should shake
    private Vector3 originPos;                              //original position of camera before shaking

    //colectibles 
    [SerializeField]
    LayerMask collectibleLayer;                              //layer of colectible things
    [SerializeField]
    float searchRadius = 0.5f;                               //radius of the overlapSphere
    Collectible cole;     
    bool jump;                                   //reference to collectible script                                        

    void Awake() {
        //getting character controller reference
        chC = GetComponent<CharacterController>();  
        //getting camera refence
        myCam = Camera.main;
        //locking mouse to the center of the screen and hiding cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //getting audio source reference from player
        myAS = GetComponent<AudioSource>();
        //playing song
        if(song != null) {
            myAS.clip = song;
            myAS.Play();
        }

        //variables initialization
        gravity = -9.81f;
        isGrounded = false;
        shake = false;
        shakeStart = 0f;
        visitedRoom = new Dictionary<string, int>();
        colected = new Dictionary<string, GameObject>();
        shakeAmount = 0.5f;
        decreaseFactor = 2.5f;
        shakeDuration = 0.5f;
    }

    
    void Update() {
        //getting input
        horVel = Input.GetAxis("Horizontal");
        verVel = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity / 100;// * Time.deltaTime;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity / 100;// * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

        if(Input.GetKeyDown(KeyCode.E)) {
            Collect();
            Interact();
        }

        //shaking camera
        if(shake) {
            if(shakeDuration < shakeStart) {
                //reseting camera position and shake parameters
                myCam.transform.localPosition = originPos;
                shake = false;
                shakeStart = 0f;
            } else {
                //shaking camera
                myCam.transform.position = originPos + Random.insideUnitSphere * shakeAmount;
                shakeStart += decreaseFactor * Time.deltaTime;
            }
        }
        if(Input.GetButtonDown("Jump")) {
            jump = true;
        }
    }

    void FixedUpdate() {
        //rotating player nad camera
        playerBody.Rotate(Vector3.up * mouseX);
        myCam.transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y + mouseX, 0f);        
        myCam.transform.rotation = Quaternion.Euler(myCam.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);

        //checking if player is qrounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //stopping adding gravity
        if(isGrounded && velocity.y < 0f) {
            velocity.y = -2f;
        }

        //player jump
        if(jump && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jump = false;
        }

        //getting player movement
        move = transform.right * horVel;
        move = verVel >= 0 ? move + transform.forward * verVel : move;

        velocity.y += gravity * Time.deltaTime;

        //moving player
        chC.Move(move * speed * Time.deltaTime + velocity * Time.deltaTime);
    }

    //function adding visit count to dictionary
    public void AddRoom(string roomName) {
        if(visitedRoom.ContainsKey(roomName)) {
            visitedRoom[roomName]++;
        } else {
            visitedRoom.Add(roomName, 1);
        }
    }
    
    //function returning number 
    public int GetValue(string roomName) {
        if(visitedRoom.ContainsKey(roomName)) {
            return visitedRoom[roomName];
        } else
            return 0;
    }

    //function enabling camera shake
    public void ShakeScreen() {
        originPos = myCam.transform.localPosition;
        shake = true;
    }

    //function givinig player's current velocity
    public Vector3 GiveVelocity() {
        
        return new Vector3(horVel, velocity.y, verVel);
    }

    private void Collect() {
        foreach (Collider col in Physics.OverlapSphere(playerBody.position, searchRadius, collectibleLayer)) {
            cole = col.gameObject.GetComponent<Collectible>();
            if(coin != null) {
                AudioSource.PlayClipAtPoint(coin, transform.position);
            }
            colected.Add(cole.GetName(), col.gameObject);
            Destroy(col.gameObject);
        }
    }
    private void Interact()
    {
        foreach (Collider col in Physics.OverlapSphere(playerBody.position, searchRadius))
        {
            if(col.tag == "Interactable")
            {
                if(button != null) {
                    AudioSource.PlayClipAtPoint(button, transform.position);
                }
                col.gameObject.GetComponent<Interactable>().Interact();
            }
        }
    }
}
