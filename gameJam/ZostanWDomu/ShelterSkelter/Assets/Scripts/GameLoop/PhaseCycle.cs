using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseCycle : GameEventListener
{
    public GameEvent[] phase;
    private int currentPhase;
    // Start is called before the first frame update
    void Start()
    {
        action.AddListener(Next);
        phase[currentPhase].Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next()
    {
        foreach(Player p in PlayerGroup.Instance().players)
        {
            p.myTurn = false;
        }
        currentPhase = (currentPhase + 1) % phase.Length;
        if (currentPhase >= 2 && currentPhase < 6)
        {
            PlayerGroup.Instance().players[currentPhase - 2].myTurn = true;
        }
        phase[currentPhase].Raise();
    }
}
