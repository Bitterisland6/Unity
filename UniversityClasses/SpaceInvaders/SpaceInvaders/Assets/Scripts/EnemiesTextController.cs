using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesTextController : MonoBehaviour
{
    public static int Enemies;
    [SerializeField]
    Text DisplayedText;

    void Start() {
        DisplayedText.text = Enemies.ToString();
    }


    void Update() {
        DisplayedText.text = Enemies.ToString();
    }


}
