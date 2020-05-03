using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    Button turn;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject[] popups;
    

    int popIndex;
    int scale;
    float waitTime;
    bool dragging;
    int panBorder;
    bool end;
    
    
    // Start is called before the first frame update
    void Start() {
        popIndex = 0;
        scale = 0;
        waitTime = 2f;
        dragging = false;
        panBorder = 3;
        end = false;
    }

    // Update is called once per frame
    void Update() {
        if(waitTime > 0) {
            waitTime -= Time.deltaTime;
        } else if(!end) {
            for(int i = 0; i < popups.Length; i++) {
                if(i == popIndex) {
                    popups[i].SetActive(true);
                } else {
                    popups[i].SetActive(false);
                }
            }

            if(popIndex == 0) {
                Time.timeScale = 0;
                //welcome text
                if(Input.GetKeyDown("space")){
                    popIndex = 1;
                }
            } else if(popIndex == 1) {
                //team
                if(Input.GetKeyDown("space")){
                    popIndex = 2;
                }
            } else if(popIndex == 2) {
                //goal
                if(Input.GetKeyDown("space")){
                    popIndex = 3;
                }
            } else if(popIndex == 3) {
                //cycle
                if(Input.GetKeyDown("space")){
                    popIndex = 4;
                }
            } else if(popIndex == 4) {
                //camera
                Time.timeScale = 1;
                if(Input.mousePosition.x >= Screen.width - panBorder || Input.GetKey(KeyCode.RightArrow))
                    popIndex = 5;
                if(Input.mousePosition.x <= panBorder || Input.GetKey(KeyCode.LeftArrow))
                    popIndex = 5;
                if(Input.mousePosition.y >= Screen.height - panBorder || Input.GetKey(KeyCode.UpArrow))
                    popIndex = 5;
                if(Input.mousePosition.y <= panBorder || Input.GetKey(KeyCode.DownArrow))
                    popIndex = 5;

                if(Input.GetMouseButton(2)) {
                    dragging = true;
                }
                if(dragging && !Input.GetMouseButton(2)) {
                    popIndex = 5;
                    dragging = false;
                }
            } else if(popIndex == 5) {
                //turns
                Time.timeScale = 0;
            } else if(popIndex == 8) {
                //cheef turn
                turn.interactable = false;
                if(Input.GetKeyDown("space")) {
                    popIndex++;
                }
            } else if(popIndex == 9) {
                //hud 1 - hearts and action points
                if(Input.GetKeyDown("space")) {
                    popIndex = 10;
                }
            } else if(popIndex == 10) {
                //hud 2 - morale
                if(Input.GetKeyDown("space")) {
                    popIndex = 11;
                }
            } else if(popIndex == 11) {
                //hud 3 - resources
                if(Input.GetKeyDown("space")) {
                    popIndex = 12;
                }
            } else if(popIndex == 12) {
                //fields - types, highlighting, actions
                if(Input.GetKeyDown("space")) {
                    popIndex = 13;
                }
            } else if(popIndex == 13) {
                //fields 2 - moving, discovering, missions
                //Time.timeScale = 0;
                if(Input.GetKeyDown("space")) {
                    popIndex = 14;
                }
            } else if(popIndex == 14) {
                //actions - types, chance of failure
                if(Input.GetKeyDown("space")) {
                    popIndex = 15;
                }
            } else if(popIndex == 15) {
                //actions - damage, extra point
                turn.interactable = true;
            } else if(popIndex == 19) {
                //weather
                turn.interactable = false;
                if(Input.GetKeyDown("space")) {
                    popIndex = 20;
                }
            } else if(popIndex == 20) {
                turn.interactable = true;
            } else if(popIndex == 22) {
                Time.timeScale = 0;
                turn.interactable = false;
            }
        }
    }

    public void ChangeIndex() {
        if((popIndex >= 5 && popIndex < 8) || (popIndex >= 15 && popIndex < 19) || (popIndex >=20 && popIndex < 22)) {
            popIndex++;
        }
    }

    public void Hide() {
        canvas.SetActive(false);
        end = true;
        Time.timeScale = 1;
    }
}
