using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loser : MonoBehaviour
{
    //references
    [SerializeField]
    AudioSource myAs;                  //Audio source playing songs
    //private references
    Animator GameOverAnimator;         //reference to animator
    MainSongController controller;     //reference to song controller

    //variables
    [SerializeField]
    AudioClip LosingSong;              //Song played when player loses game 
    [SerializeField]
    TextMeshProUGUI GameOverText;      //text displayed when player dies
    
    public RestartGame restartManager; //reference needed to call restart function

    //function losing game
    public void LoseGame() {
        //allowing restart
        restartManager.restart();
        //getting animator reference
        GameOverAnimator = GameOverText.GetComponent<Animator>();
        //playing text animation
        GameOverAnimator.SetTrigger("GameOver");
        //getting reference to song controller
        controller = myAs.GetComponent<MainSongController>();
        //changing song
        controller.PlaySong(LosingSong, MainSongController.Volume);
        //stopping game
        Time.timeScale = 0;
    }
}
