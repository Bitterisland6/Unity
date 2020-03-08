using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    //function loading main menu
    public void GoToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
