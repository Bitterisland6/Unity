using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Discription : MonoBehaviour
{
    TextMeshProUGUI text;
    public GameObject discBox;
    // Start is called before the first frame update
    void Start()
    { 
        text = discBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    
    public void SetActive(string text)
    {
        discBox.SetActive(true);
        this.text.SetText(text);
    }
    public void DeActivate()
    {
        discBox.SetActive(false);
    }
}
