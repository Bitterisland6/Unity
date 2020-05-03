using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeatherType", menuName = "ScriptableObjects/WeatherType", order = 4)]
public class WeatherType : ScriptableObject
{
    public Sprite icon;
    [TextArea]
    public string discription;
    [Header("bsed on shelter")]
    public bool requireShelter;
    public int hpPenalty;
    public int moralePenalty;
    [Header("not based on shelter")]
    public int moraleChange;
}
