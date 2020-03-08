using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    //variables
    public static int WholeGameScore;       //player's score from whole game
    public static int CurrentLevelScore;    //player's score in current level
    [SerializeField]
    TextMeshProUGUI ScoreText;              //score text displayed on player's canvas

    
    void Start() {
        CurrentLevelScore = 0;
        ScoreText.text = "Score: " + CurrentLevelScore;
    }

    void Update() {
        //updating displayed score
        ScoreText.text = "Score: " + CurrentLevelScore;
    }
}
