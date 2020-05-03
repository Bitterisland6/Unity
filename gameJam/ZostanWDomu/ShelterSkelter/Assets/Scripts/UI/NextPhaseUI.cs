using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhaseUI : MonoBehaviour
{
    public GameEvent endTurn;
    public void EndTurn()
    {
        endTurn.Raise();
    }
}
