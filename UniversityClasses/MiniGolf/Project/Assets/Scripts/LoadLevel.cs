using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    //private variables
    int scenesNumber;   //number of scenes
    int sceneIndex;     //index of current scene

    void Start() {
        //getting index of current scene
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //getting number of all scenes
        scenesNumber = SceneManager.sceneCountInBuildSettings;
    }

    //function loading next scene by index in build settings
    public void Load() {
        SceneManager.LoadScene((sceneIndex + 1) % scenesNumber);
    }

}