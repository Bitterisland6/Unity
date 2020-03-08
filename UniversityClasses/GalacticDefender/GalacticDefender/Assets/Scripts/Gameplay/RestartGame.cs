using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    //variables
    private bool restartPossible = false;   //bool saying if we can restart game
    private int SceneIndex;                 //index of currently loaded scene

    void Awake() {
        //starting game
        Time.timeScale = 1;
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update() {
        if(restartPossible) {
            //restarting when player accepts it
            if(Input.GetButtonDown("Submit")) {
                Time.timeScale = 1;
                //loading same scene
                SceneManager.LoadScene(SceneIndex, LoadSceneMode.Single);
                //ressetting enemies text
                EnemiesTextController.Enemies = 0;
            }
        }
    }

    //function making restart possible
    public void restart(){
        restartPossible = true;
    }
}
