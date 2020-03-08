using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemiesTextController : MonoBehaviour
{
    //variables
    [SerializeField]
    TextMeshProUGUI DisplayedText;          //Displayed number of enemies
    
    public static int Enemies;              //number of enemies on level
    

    void Awake() {
        //setting displayed text to number of enemies
        DisplayedText.text += Enemies.ToString();
    }

    void Update() {
        //updating text
        DisplayedText.text = Enemies.ToString();
    }
}
