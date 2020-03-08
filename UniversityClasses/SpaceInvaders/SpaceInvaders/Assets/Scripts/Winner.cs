using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winner : MonoBehaviour
{
    private bool didwin;    //flag needed to play winning effects only once

    [SerializeField]
    Text WinningText;       //text displayed when we win

    [SerializeField]
    AudioSource myAs;
    [SerializeField]
    AudioClip WinningSong; 

    public RestartGame restartManager;

    void Awake() {
        didwin = false;
    }

    void Update() {
        if(EnemiesTextController.Enemies == 0 && !didwin) {
            didwin = true;
            WinGame();
        }
    }


    public void WinGame() {
        restartManager.restart();
        Animator WinGameAnimator = WinningText.GetComponent<Animator>();
        WinGameAnimator.SetTrigger("GameOver");
        MainSongController controller = myAs.GetComponent<MainSongController>();
        controller.PlaySong(WinningSong, 1f);
        Time.timeScale = 0;
    }
}
