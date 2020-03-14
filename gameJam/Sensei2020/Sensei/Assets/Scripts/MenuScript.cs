using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject popup_window;
    
    void Awake() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PressStartButton()
    {
        popup_window.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Cancel_Quit()
    {
        popup_window.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
