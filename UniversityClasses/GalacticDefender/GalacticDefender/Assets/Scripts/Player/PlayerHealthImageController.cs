using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthImageController : MonoBehaviour
{
    //variables
    [SerializeField]
    Image TargetImage;              //target image on players hud UI
    [SerializeField]
    Sprite FullHealthImg;           //image displayed when player has full health
    [SerializeField]
    Sprite MediumHealthImg;         //image displayed when player lost some health
    [SerializeField]
    Sprite LowHealthImg;            //image displayed when player is near to death
    
    void Awake() {
        //setting correct image
        TargetImage.sprite = FullHealthImg;
    }

    void Update() {
        //updating image
        if(PlayerHealth.PlayerHealthNum == 2) {
            TargetImage.sprite = MediumHealthImg;
        } else if(PlayerHealth.PlayerHealthNum == 1) {
            TargetImage.sprite = LowHealthImg;
        }
    }
}
