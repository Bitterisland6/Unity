using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootBall : MonoBehaviour
{
    //references
    [SerializeField]
    GameObject Ball;            //refeence to ball itself
    LineRenderer lr;            //reference to linerenderer
    
    //variables
    [Range(1f, 40f)]
    public float Power = 15f;   //multiplier of shot power
    [SerializeField]
    AnimationCurve AC;          //animation curve of drawed direction line
    [SerializeField]
    Color StartingColor;        //color at the begining of the direction line
    [SerializeField]
    Color EndingColor;           //color at the end of the dorection line

    //private variables
    float maxPower = 30f;       //maximum power of shot
    float currentPower;         //current shot of power (during drag)
    float shotPower;            //power of shot
    Vector3 startingPos;        //start position of mouse drag
    Vector3 endPos;             //current end position of mouse drag
    Vector3 direction;          //shot direction
    Vector3 goodEndPos;         //last correct end position of mouse drag (in a right distance)
    Vector3 drawingEnd;         //end of direction line
    Vector3 position;           //position of mouse click
    Rigidbody ballRb;           //ball's rigidbody
    bool dragging = false;      //varaible checking our dragging state
    Ray ray;                    //ray needed to get mouse click position
    RaycastHit hit;             //hit of the ray
    Behaviour halo;             //glowing of the ball


    void Start() {
        //getting rigidbody from the ball
        ballRb = Ball.GetComponent<Rigidbody>();
        //getting linerenderer form the ball
        lr = Ball.GetComponent<LineRenderer>();
        lr.positionCount = 2; //setting nummber of points for line renderer to draw line through
        lr.numCapVertices = 10; //making direction line looking round
        lr.useWorldSpace = true; //direction line rendered around world origin
        lr.widthCurve = AC; //using my own curve animation
        lr.enabled = false; //hiding direction line
        halo = (Behaviour)GetComponent("Halo"); //getting glowing component
    }

    void Update() {
        //if ball is not moving
        if(ballRb.velocity == Vector3.zero) {
            //starting ball's glow
            halo.enabled = true;
            //if player is draging
            try {
                WarningLight.Warning = false;
                if(dragging) {
                    InDrag(); //doing things which should be done during drag
                    if(Input.GetMouseButtonDown(1)) 
                        //if player presses right mouse button - cancel drag
                        CancelDrag();
                    if(!Input.GetMouseButton(0)){ 
                        //if player is not pressing mouse button - end drag
                        EndDrag();
                    } 
                } else {
                    if(Input.GetMouseButton(2))
                        //enabling camera rotation
                        CameraFollow.canRotate = true;
                    else
                        //disabling camera rotation
                        CameraFollow.canRotate = false;
                    
                    //if player is not draging and presses mouse button - start dragging
                    if(Input.GetMouseButtonDown(0)){
                        StartDrag();
                    }
                }
            } catch(System.Exception) { //not gettin raycast hit
                WarningLight.Warning = true;
            }
        } else if (ballRb.velocity.magnitude < 0.001) {
            //stopping the ball if it's moving really slowly
            ballRb.velocity = Vector3.zero;
        }  
            
    }

    //function which starts drag (start of state)
    void StartDrag() {
        //getting start point of mouse drag
        startingPos = GetMouseWorldPosition();
        //starting drag
        dragging = true;
    }

    //function which is drawing direction line and getting ending point during mouse drag (in state)
    void InDrag() {
        //getting ending point of drag
        endPos = GetMouseWorldPosition();
        //getting correct ending point (not too far away from starting point)
        if(Vector3.Distance(startingPos, endPos) < 2f)
            goodEndPos = endPos;
        //setting drawing end in right distance to starting point
        drawingEnd = Vector3.Normalize(startingPos - endPos) * Vector3.Distance(startingPos, goodEndPos);
        //setting points for line renderer
        lr.SetPosition(0, ballRb.position);
        lr.SetPosition(1, ballRb.position + drawingEnd);
        //counting power of shot, based by current ending position
        currentPower = Vector3.Distance(startingPos, goodEndPos) * Power;
        //drawing line
        lr.enabled = true;
        //setting right color
        lr.material.color = Color.Lerp(StartingColor, EndingColor, currentPower/maxPower); 
        //disabling camera rotation
        CameraFollow.canRotate = false;      
    }

    //function that ends mouse drag (end of state)
    void EndDrag() {
        //ending ball's glow
        halo.enabled = false;
        //counting helping parameters like direction and power
        direction = Vector3.Normalize(startingPos - endPos);
        shotPower = Vector3.Distance(startingPos, goodEndPos) * Power;
        //shooting ball
        ballRb.AddForce(direction * shotPower, ForceMode.Impulse);
        //ending drag
        dragging = false;
        //hiding direction line
        lr.enabled = false;
        //adding point to current level score
        ScoreController.CurrentLevelScore++;
        //DŻWIĘK UDERZENIA PIŁKI
    }

    //function that cancels mouse dragging
    void CancelDrag() {
        //ending drag
        dragging = false;
        //hiding direction line
        lr.enabled = false;
    }


    //function getting position of mouse click 
    Vector3 GetMouseWorldPosition(bool ballOnly = false) {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit)) {
            if(!ballOnly || hit.transform.gameObject == gameObject) {
                position = hit.point;
                position.y = ballRb.position.y;
                return position;
            }
        }
        throw new System.Exception();
    }
}
