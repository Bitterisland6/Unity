using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderColorController : MonoBehaviour
{
    //variables
    private Slider slider; //reference to the slider
    public Image fill; //fill of the slider
    public Color maxValueColor; //color when slider has max value
    public Color minValueColor; //color when slider has min value

    void Start() {
        //HUD initialization
        slider = gameObject.GetComponent<Slider>();
        fill.color = maxValueColor;
    }

    //seting slider color
    void FixedUpdate() {
        float currentValue = slider.value;
        float maxValue = slider.maxValue;
        fill.color = Color.Lerp(minValueColor, maxValueColor, currentValue/maxValue);        
    }
}
