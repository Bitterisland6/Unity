using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //variables
    public static int SceneIndex;           //index of a scene to load on pressing play buton

    void Awake() {
        //initializing variables
        SceneIndex = 2;
    }
    
    //function loading game level
    public void PlayGame() {
        SceneManager.LoadScene(SceneIndex);
    }

    //function loading tutorial
    public void PlayTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    //function quiting game
    public void QuitGame() {
        Application.Quit();
    }
}
