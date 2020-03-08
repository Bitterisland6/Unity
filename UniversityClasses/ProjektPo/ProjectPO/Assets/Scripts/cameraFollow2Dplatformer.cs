using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow2Dplatformer : MonoBehaviour
{
    //variables
    public Transform target; //what is camera following
    public float smoothing; //some kind of delay of the follow

    private Vector3 offset; //distance of the camera from the player
    private float lowY; //the lowest point camera is able to go
    
    void Start() {
        //current position of the camera - position of the target
        offset = transform.position - target.position;
        //current position of the camera
        lowY = transform.position.y;
        
    }

    //calculate actual camera position
    void FixedUpdate() {
        //new camera position
        Vector3 targetCamPos;
        //checking if player is alive and setting camera position
        if(target != null)
            targetCamPos = target.position + offset;
        else
            targetCamPos = transform.position;
        //transforming camera position, Lerp makes it smoothly
        //deltaTime makes it move slowly
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing*Time.deltaTime);
        //to secure that camera doesn't go lower than lowY
        if(transform.position.y < lowY)
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        //when player dies, camera stays in place
    }
}
