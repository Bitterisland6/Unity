using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Winner : MonoBehaviour
{
    //references
    public RestartGame restartManager;      //reference needed to make restart possible
    Animator WinGameAnimator;               //reference needed to animate text
    MainSongController controller;          //reference needed to play wining song

    //variables
    [SerializeField]
    TextMeshProUGUI WinningText;            //text displayed when we win
    [SerializeField]
    AudioSource myAs;                       //given audiosource to play wining song
    [SerializeField]
    AudioClip WinningSong;                  //song played when player wins

    //private variables
    private bool didWin;                    //bool checking if player won game

    void Awake() {
        //initializing variables
        didWin = false;
    }

    void Update() {
        //if player destroyed all enemies and have not win yet
        if(EnemiesTextController.Enemies == 0 && !didWin) {
            //saying that player won, and calling winning game function
            didWin = true;
            WinGame();
        }
    }

    //function making actions when player wins game
    public void WinGame() {
        //making restart possible
        restartManager.restart();
        //getting animator reference
        WinGameAnimator = WinningText.GetComponent<Animator>();
        //starting text animation
        WinGameAnimator.SetTrigger("GameOver");
        //getting reference to change song
        controller = myAs.GetComponent<MainSongController>();
        //playing wining song
        controller.PlaySong(WinningSong, MainSongController.Volume);
        //pausing game
        Time.timeScale = 0;
    }
}
