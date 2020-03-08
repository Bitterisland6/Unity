using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //references
    [SerializeField]
    Transform target;                       //target for the camera to follow
    
    public Transform originCameraAttractor; //objects which camera looks on
    public Transform cameraAttractor;
    
    //variables
    [SerializeField][Range(0.01f, 10.0f)]
    float velocityFactor;                   //factor of the camera velocit

    [SerializeField][Range(0.01f, 10.0f)]
    float rotationSpeed;                    //spped of camera rotation
    public bool canRotate;                  //bool saying if player can rotate camera
    
    //private variables
    Vector3 cameraOffset;                   //offset of the camera
    Vector3 attractorOffset;                //offset of the attractor
    Vector3 velocity;                       //camera's move velocity
    Quaternion cameraRotation;              //rotation of the camera taken form mouse input
    bool inventoryCam;                      //flag saying if camera should be in inventory mode
    bool focused;                           //flag saying if camera should be in focused mode
    float yRotation;
    
    void Awake() {
        //counting camera offset based by starting player's position and camera's position
        attractorOffset = cameraAttractor.position - target.position;
        //initiating variables;
        velocityFactor = 5.0f;
        rotationSpeed = 2.0f;
        canRotate = true;
        inventoryCam = false;
        focused = false;
    }

    void Update() {
            //getting rotation from mouse input
            if(canRotate) {
                cameraRotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
            }
    }

    void FixedUpdate() {
        //updating camera's velocity and position
        UpdateVelocity();
        UpdatePosition();
        yRotation = Mathf.Lerp(yRotation, 0, 0.01f);
        yRotation = Mathf.Clamp(yRotation, -20, 40);
        originCameraAttractor.position = target.position + attractorOffset;
        
        if(!inventoryCam) {
            if (cameraAttractor != null)
            {
                transform.LookAt(cameraAttractor.position);
                if(!focused)
                {
                    transform.eulerAngles = new Vector3(yRotation, transform.eulerAngles.y, 0);
                }
            }
        }
        else
        {
            transform.position = originCameraAttractor.position + Vector3.up * 1f;
            transform.LookAt(target);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        if (canRotate && !focused) {
            //applying rotation to camera and attractor offsets
            attractorOffset = cameraRotation * attractorOffset;
            
            //rotating attractor around player
            cameraAttractor.RotateAround(target.position, Vector3.up, cameraRotation.eulerAngles.y);
        }
    }

    //function updating camera's velocity based by distance between target and camera
    void UpdateVelocity() {
        if (cameraAttractor == null) return;
        cameraOffset = (target.position - cameraAttractor.position) * 1.3f + Vector3.up * 2 + (target.position - cameraAttractor.position).normalized;
        velocity = ((cameraAttractor.position + cameraOffset) - transform.position) * velocityFactor;
    }

    //function updating camera's position
    void UpdatePosition() {
        transform.position += velocity * Time.deltaTime;
    }

    public void InventoryCamera() {
        inventoryCam = !inventoryCam;
    }

    public void Focus(Transform target) {
        focused = true;
        cameraAttractor = target;
    }

    public void DeFocus() {
        focused = false;
        cameraAttractor = originCameraAttractor;
    }
}