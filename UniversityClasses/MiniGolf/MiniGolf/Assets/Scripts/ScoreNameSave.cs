using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreNameSave : MonoBehaviour
{
    //variables
    [SerializeField]
    TMP_InputField NameInput;
    [SerializeField]
    Button SubmitButton;
    [SerializeField]
    GameObject InputManager;

    bool saved;

    void Awake() {
        InputManager.SetActive(false);
        saved = false;
    }

    void Update() {
        if(EndLevel.WonGame) 
            InputManager.SetActive(true);
        if(saved)
            InputManager.SetActive(false);
    }
    
    public void GetAndSaveText() {
        if(!saved) {
            HighScoreTable.AddEntry(ScoreController.WholeGameScore, NameInput.text);
            saved = true;
        }
    }
}
