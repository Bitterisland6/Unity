using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    //function setting difficulty level to easy (loading right scene)
    public void SetEasy() {
        MainMenu.SceneIndex = 1;
    }
    
    //function setting difficulty level to medium (loading right scene)
    public void SetMedium() {
        MainMenu.SceneIndex = 2;
    }

    //function setting difficulty level to hard (loading right scene)
    public void SetHard() {
        MainMenu.SceneIndex = 3;
    }
}
