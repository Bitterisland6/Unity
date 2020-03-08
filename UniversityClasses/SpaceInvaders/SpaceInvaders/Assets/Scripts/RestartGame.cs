using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    //variables
    private bool restartPossible = false; //bool saying if we can restart game

    void Awake() {
        Time.timeScale = 1;
    }

    void Update() {
        if(restartPossible) {
            if(Input.GetButtonDown("Submit")) {
                Time.timeScale = 1;
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                EnemiesTextController.Enemies = 0;
            }
        }
    }

    public void restart() {
        restartPossible = true;
    }

}
