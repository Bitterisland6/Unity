using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //variables
    [SerializeField]
    Transform Target;                       //target which camera will follow
    [SerializeField]
    float FollowTime;                       //how long will it take to get the camera to target's position
    [SerializeField]
    float startThreshold;                   //distance needed to start follow
    [SerializeField]
    float RotationSpeed = 5f;               //speed of camera rotation

    public static bool canRotate;    //bool saying if we can rotate the camera
    
    //private variables
    Vector3 cameraOffset;                   //offset of the camera
    Vector3 previousPosition;               //previous position of the camera
    float startTime;                        //time when camera started moving
    bool follow;                    //bool saying if camera is moving
    Vector3 velocity;                       //camera's velocity
    
    

    void Start() {
        //setting previous position of the camera to starting camera position
        previousPosition = transform.position;
        //counting camera offset
        cameraOffset = transform.position - Target.position;
        
        follow = false;
        canRotate = true;
    }
    
    void LateUpdate() {
        //if player can rotate the camera
        if(canRotate) {
            //getting rotation from mouse input
            Quaternion CameraRotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
            //applying rotation to camera offset
            cameraOffset = CameraRotation * cameraOffset;
        }
        
        //if camera should follow target
        if(follow) {
            //counting difference between current time and start of follow time
            float delta = Time.time - startTime;
            //interpolating camera position by time
            transform.position = Vector3.Lerp(previousPosition, Target.position + cameraOffset, delta / FollowTime);
            //ending follow
            if(delta > FollowTime) {
                follow = false;
                //Debug.Log("end of follow");
            }
        } else {
            //if object is too far from camera -> start follow
            if (Vector3.Magnitude(transform.position - Target.position + cameraOffset) > startThreshold) {
                follow = true;
                //Debug.Log("start of follow");
                StartFollow();
            }
        }
        //making camera look at target
        transform.LookAt(Target);            
    }

    //function that starts camera follow -> sets time of follow start and updates previous camera position
    void StartFollow() {
        previousPosition = transform.position;
        startTime = Time.time;
    }
}
