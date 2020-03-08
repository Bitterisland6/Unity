using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
   [SerializeField]
   GameObject[] Texts;

    //private variables
    int popUpIndex;
    float waitTime;

    void Start() {
        SpawnEnemies.canAnimate = false;
        popUpIndex = 0;
        waitTime = 15f;
        //Time.timeScale = 0;
    }

    void Update() {
        if(waitTime > 0){
            Debug.Log(waitTime);
            Debug.Log(Time.deltaTime);
            waitTime -= Time.deltaTime;
        } else {
            for(int i = 0; i < Texts.Length; i++) {
                if(i == popUpIndex) {
                    Texts[i].SetActive(true);
                    Texts[i].GetComponent<TypeWriterEffect>().StartWriting();
                }
                else
                    Texts[i].SetActive(false);
            }

            if(popUpIndex == 0){    
                //Welcome text
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            }else if(popUpIndex == 1) {
                //WSAD moving
                Time.timeScale = 1;
                if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 2) {
                //celowanie
                if(Input.GetAxis("HorizontalRotate") != 0 || Input.GetAxis("VerticalRotate") != 0) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 3) {
                //strzał
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 4) {
                //kamera od pocisków
                //Time.timeScale = 0;
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 5) {
                //celownik
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 6) {
                //gdzie przeciwnicy
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 7) {
                //życie
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 8) {
                //liczba przeciwników
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 9) {
                //wyjście do menu
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                }
            } else if(popUpIndex == 10) {
                //jakieś zabij przeciwników
                if(Input.GetKeyDown("space")) {
                    popUpIndex++;
                    Time.timeScale = 1;
                    SpawnEnemies.canAnimate = true;
                }
            }
        }
    }
}
