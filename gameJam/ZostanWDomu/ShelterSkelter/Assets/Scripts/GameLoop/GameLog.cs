using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLog : GameEventListener
{
    [TextArea]
    public string[] dayLog;
    public GameObject logObject;
    public TMPro.TextMeshProUGUI text;
    private int day;
    private string addText;
    private static GameLog instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        addText = "";
        action.AddListener(()=>Invoke("DayUpdate",0.3f));
    }

    // Update is called once per frame
    void DayUpdate()
    {
        text.text = "Day "+day.ToString()+"\n"+dayLog[day]+"\n"+addText;
        day++;
        logObject.SetActive(true);
        addText = "";
    }
    public void AddText(string txt)
    {
        addText += "\n" + txt;
    }
    public GameLog Instance()
    {
        return instance;
    }
}
