using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartGame : MonoBehaviour
{
    //variables
    private bool restartNow = false; //bool which will say if we can restart the game
    

    // Update is called once per frame
    void Update() {
        //if we can restart game
        if(restartNow) {
            if(Input.GetButtonDown("Submit")) {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    //function wich will allow us to restart the game
    public void restart() {
        restartNow = true;
    }
}
