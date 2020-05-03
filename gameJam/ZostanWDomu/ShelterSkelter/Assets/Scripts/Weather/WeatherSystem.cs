using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : GameEventListener
{
    public Weather_Controller visual;
    public Weather_Cloudy cloudy;
    public Weather_Rain rain;
    public Weather_Thunderstorm storm;
    public Weather_Snow snow;
    public Weather_Sun sun;
    public GameObject rainParticle;
    public GameObject snowParticle;
    
    public AudioClip[] clip;
    public WeatherType[] weatherType;
    static WeatherSystem instance;
    private Queue<WeatherType> forecast;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        action.AddListener(UpdateWeather);
        forecast = new Queue<WeatherType>();
        forecast.Enqueue(weatherType[0]);
        BgAudioSystem.Instance().ChangeBg(clip[0]);
    }
    public static WeatherSystem Instance()
    {
        if(instance == null)
        {
            Debug.Log("WeatherSystem missing");
        }
        return instance;
    }
    private void UpdateWeather()
    {
        ApplyWeather(forecast.Dequeue());
        int i = Random.Range(0, weatherType.Length);
        forecast.Enqueue(weatherType[i]);
        BgAudioSystem.Instance().ChangeBg(clip[i]);
        switch(i)
        {
            case 0:
                ResetWeather();
                cloudy.enabled = true;
                break;
            case 1:
                ResetWeather();
                storm.enabled = true;
                rainParticle.SetActive(true);
                break;
            case 2:
                ResetWeather();
                rain.enabled = true;
                rainParticle.SetActive(true);
                break;
            case 3:
                ResetWeather();
                snow.enabled = true;
                snowParticle.SetActive(true);
                break;
            case 4:
                ResetWeather();
                sun.enabled = true;
                break;
            case 5:
                ResetWeather();
                cloudy.enabled = true;
                break;
        }
    }
    public WeatherType Forecast(int i)
    {
        return forecast.ToArray()[i];
    }
    private void ApplyWeather(WeatherType weather)
    {
        Debug.Log(weather.discription);
        foreach(Player p in PlayerGroup.Instance().players)
        {
            if(weather.requireShelter && !TileMap.Instance().tileMap[p.pos.x,p.pos.y].shelter)
            {
                p.OnDamaged(weather.hpPenalty);
                p.OnMoraleChange(-weather.moralePenalty);
            }
            p.OnMoraleChange(weather.moraleChange);
            
        }
    }
    private void ResetWeather()
    {
        rainParticle.SetActive(false);
        snowParticle.SetActive(false);
        cloudy.enabled = false;
        rain.enabled = false;
        storm.enabled = false;
        snow.enabled = false;
        sun.enabled = false;
    }
}
