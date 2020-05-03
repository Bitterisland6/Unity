using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseUIUpdater : GameEventListener
{
    public GameObject[] playerIcon;
    public GameOver game;
    public ToD_Base timeVisual;
    public GameEvent[] phase;
    public string[] phaseDisplayedName;
    public int currentId;
    private TMPro.TextMeshProUGUI txt;
    private int day;
    private void Start()
    {
        action.AddListener(Next);
        txt = GetComponent<TMPro.TextMeshProUGUI>();
        txt.text = phaseDisplayedName[currentId] + $"\nday {day}";
        phase[currentId].Raise();
        Debug.Log(currentId);
    }
    private void Next()
    {
        for(int i = 0; i < 4; i++)
        {
            
        }
        if (currentId == phaseDisplayedName.Length - 1)
        {
            day++;
            if(day == 15)
            {
                game.EndGame(true);
            }
        }
        currentId = (currentId + 1) % phaseDisplayedName.Length;
        foreach(Player p in PlayerGroup.Instance().players)
        {
            p.myTurn = false;
        }
        if (currentId >= 2 && currentId < 6)
        {
            PlayerGroup.Instance().players[currentId - 2].myTurn = true;
            
        }
        txt.text = phaseDisplayedName[currentId] + $"\nday {day}";
        phase[currentId].Raise();
        switch(currentId)
        {
            case 0:
                timeVisual.Set_fCurrentTimeOfDay = 9;
                break;
            case 1:
                timeVisual.Set_fCurrentTimeOfDay = 10;
                break;
            case 2:
                timeVisual.Set_fCurrentTimeOfDay = 11;
                break;
            case 3:
                timeVisual.Set_fCurrentTimeOfDay = 13;
                break;
            case 4:
                timeVisual.Set_fCurrentTimeOfDay = 14;
                break;
            case 5:
                timeVisual.Set_fCurrentTimeOfDay = 15;
                break;
            case 6:
                timeVisual.Set_fCurrentTimeOfDay = 16;
                break;
            case 7:
                timeVisual.Set_fCurrentTimeOfDay = 18;
                break;
            case 8:
                timeVisual.Set_fCurrentTimeOfDay = 22;
                break;
        }
        
    }
}
