using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningLight : MonoBehaviour
{
    //variables
    [SerializeField]
    Sprite GoodLight;           //sprite shown, when there's no need to warn player
    [SerializeField]
    Sprite BadLight;            //sprite warning player
    [SerializeField]
    Image TargetImage;          //target image that will display sprites
    
    public static bool Warning = false;    //bool saying if we should warn player


    void Start() {
        //setting displayed image to green light
        TargetImage.sprite = GoodLight;
    }


    void Update() {
        //image change based by Warning bool
        TargetImage.sprite = Warning ? BadLight : GoodLight;
    }
}
