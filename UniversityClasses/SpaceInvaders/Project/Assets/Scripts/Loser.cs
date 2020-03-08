using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loser : MonoBehaviour
{
    //restart game
    public RestartGame restartManager; //reference needed to call restart function
    [SerializeField]
    Text GameOverText;                 //text displahyed on screen when player dies

    [SerializeField]
    AudioSource myAs;                  //reference to Audio Source playing sound
    [SerializeField]
    AudioClip LosingSong;              //clip played when player loses game

    //function making all things to show that player lost game
    public void LoseGame() {
        //making restart possible
        restartManager.restart();
        //showing text
        Animator GameOverAnimator = GameOverText.GetComponent<Animator>();
        GameOverAnimator.SetTrigger("GameOver");
        //playing right clip
        MainSongController controller = myAs.GetComponent<MainSongController>();
        controller.PlaySong(LosingSong, 1f);
        //stopping game
        Time.timeScale = 0;
    }
}
