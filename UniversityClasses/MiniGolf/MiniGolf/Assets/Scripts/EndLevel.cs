using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLevel : MonoBehaviour
{
    //references
    [SerializeField]
    GameObject Loader;              //reference needed to load next level
    
    //variables
    [SerializeField]
    GameObject ConfettiPS;          //confetti efect spawned when ball gets in the hole
    [SerializeField]
    Transform SpawnPlace1;          //spawn place number one for confetti
    [SerializeField]
    Transform SpawnPlace2;          //spawn place number two for confetti
    [SerializeField]
    TextMeshProUGUI WiningText;     //text displayed on lvl end
    [SerializeField]
    AudioClip WinAudio;             //audio played on lvl end

    public static bool WonGame;                   //bool saying if player ended level

    //private variables
    LoadLevel load;                 //LoadLevel script reference needed to call Load() function
    private AudioSource AS;         //audio source needed to play winning sound

    void Start() {
        //getting LoadLevel reference and audio source from game object
        load = Loader.GetComponent<LoadLevel>();
        AS = GetComponent<AudioSource>();
        WonGame = false;
    }

    void Update() {
        //loading next level on "enter" click when player ended current level
        if(WonGame) {
            if(Input.GetButtonDown("Submit")) {
                Time.timeScale = 1f;
                load.Load();
            }
        }
    }


    void OnTriggerEnter(Collider other) {
        //winning game if player hit the hole
        if(other.tag == "Ball")
            WinLevel();
    }

    //funvtion that does all staf when player wins 
    private void WinLevel() {
        //spawning confettis
        Instantiate(ConfettiPS, SpawnPlace1.position, Quaternion.identity);
        Instantiate(ConfettiPS, SpawnPlace2.position, Quaternion.identity);
        //displaying text
        Animator EndLvlAnimator = WiningText.GetComponent<Animator>();
        EndLvlAnimator.SetTrigger("EndLevel");
        //adding level score to score from whole game
        ScoreController.WholeGameScore += ScoreController.CurrentLevelScore;
        //saying that player ended level
        WonGame = true;
        AS.clip = WinAudio;
        AS.Play();
        //pausing game
        Time.timeScale = 0f;
    }
}
