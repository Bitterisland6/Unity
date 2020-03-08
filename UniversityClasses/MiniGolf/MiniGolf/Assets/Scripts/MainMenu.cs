using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake() {
        //reseting score from whole game
        ScoreController.WholeGameScore = 0;
    }

    public void PlayGame() {
        //loading lvl1 
        SceneManager.LoadScene("lvl1");
    }

    public void QuitGame() {
        //quiting app
        Application.Quit();
    }
}
