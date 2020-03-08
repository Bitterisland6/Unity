using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //variables
    [SerializeField]
    GameObject[] Texts;

    //private variables
    int popUpIndex;
    float waitTime;

    void Start() {
        //initializating variables
        SpawnEnemies.canAnimate = false;
        popUpIndex = 0;
        waitTime = 3f;
        //stopping game
        Time.timeScale = 0;
    }

    void Update() {
        //checking if we can start our tutorial
        if(waitTime > 0){
            waitTime -= Time.unscaledDeltaTime;
        } else {
            //making visible only right popup
            for(int i = 0; i < Texts.Length; i++) {
                if(i == popUpIndex) {
                    //making popup visible
                    Texts[i].SetActive(true);
                    //animating text
                    Texts[i].transform.GetChild(1).GetComponent<TypeWriterEffect>().StartWriting();
                } else
                    Texts[i].SetActive(false);
            }

            //displaying and checking actions for each tutorial step
            if(popUpIndex == 0){    
                //Welcome text
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            }else if(popUpIndex == 1) {
                //WSAD moving
                if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
                    Time.timeScale = 1;
                    popUpIndex++;
                }
            } else if(popUpIndex == 2) {
                //aiming
                if(Input.GetAxisRaw("HorizontalRotate") != 0 || Input.GetAxisRaw("VerticalRotate") != 0) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 3) {
                //shooting
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 4) {
                //LeftCamera
                Time.timeScale = 0;
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 5) {
                //crosshair
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 6) {
                //camera locating enemies
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 7) {
                //health indicator
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 8) {
                //number of enemies
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 9) {
                //exit to menu
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 10) {
                //ending tutorial
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                    Time.timeScale = 1;
                    SpawnEnemies.canAnimate = true;
                }
            }
        }
    }
}
