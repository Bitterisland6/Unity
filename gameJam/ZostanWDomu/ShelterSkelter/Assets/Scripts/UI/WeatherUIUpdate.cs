using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeatherUIUpdate : GameEventListener
{
    //icon
    [SerializeField]
    Image weatherIcon;
    
    //description
    [SerializeField]
    TextMeshProUGUI description;
    
    //require shelter
    [SerializeField]
    Image shelter;
    
    //hpPenalty
    [SerializeField]
    Image dmg;
    [SerializeField]
    TextMeshProUGUI dmgText;
    
    //morale
    [SerializeField]
    Image[] moraleChange;
    [SerializeField]
    Image moraleDown;
    [SerializeField]
    TextMeshProUGUI moralePenaltyText;
    [SerializeField]
    TextMeshProUGUI moraleChangeText;
    
    
    //private variables
    WeatherType returnedWeather;

    void Awake() {
        action.AddListener(() => Invoke("UpdateWeatherUI", 0.1f));
    }
    private void Start()
    {
        UpdateWeatherUI();
    }

    public void UpdateWeatherUI() {
        if(WeatherSystem.Instance().Forecast(0) == null) {
            return;            
        } else {
            returnedWeather = WeatherSystem.Instance().Forecast(0);
            UpdateIcon();
            UpdateDesc();
            UpdateHp();
            UpdateMoralePen();
            UpdateMoraleChange();
        }
    }

    private void UpdateIcon() {
        if(returnedWeather != null) {
            weatherIcon.sprite = returnedWeather.icon;
        }
    }

    private void UpdateDesc() {
        if(returnedWeather != null) {
            description.text = returnedWeather.discription;
        }
    }

    private void UpdateMoralePen() {
        if(returnedWeather != null) {
            //requre shelter
            shelter.gameObject.SetActive(returnedWeather.requireShelter);
            //penalty
            if(returnedWeather.moralePenalty != 0) {
                moraleDown.gameObject.SetActive(true);
                moralePenaltyText.gameObject.SetActive(true);
                moralePenaltyText.text = (returnedWeather.moralePenalty * (-1)).ToString();
            } else {
                moraleDown.gameObject.SetActive(false);
                moralePenaltyText.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateMoraleChange() {
        if(returnedWeather != null){
            foreach(Image img in moraleChange) {
                img.gameObject.SetActive(false);
            }

            if(returnedWeather.moraleChange < 0) {
                moraleChange[0].gameObject.SetActive(true);
                moraleChangeText.gameObject.SetActive(true);
                moraleChangeText.text = (returnedWeather.moraleChange * (-1)).ToString();
            } else if(returnedWeather.moraleChange > 0) {
                moraleChange[1].gameObject.SetActive(true);
                moraleChangeText.gameObject.SetActive(true);
                moraleChangeText.text = returnedWeather.moraleChange.ToString();
            } else{
                moraleChangeText.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateHp() {
        if(returnedWeather != null) {
            if(returnedWeather.hpPenalty != 0) {
                dmg.gameObject.SetActive(true);
                dmgText.gameObject.SetActive(true);
                dmgText.text = (returnedWeather.hpPenalty * (-1)).ToString();
            } else {
                dmg.gameObject.SetActive(false);
                dmgText.gameObject.SetActive(false);
            }
        }
    }
}
