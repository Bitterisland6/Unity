using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomEnterTimes : MonoBehaviour
{
    public TextMeshProUGUI text;
    

    public void UpdateCounter(string roomName)
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        text.text = player.GetValue(roomName).ToString();
    }
}
