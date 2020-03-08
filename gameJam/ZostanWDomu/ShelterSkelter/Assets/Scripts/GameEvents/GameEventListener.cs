using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent listenedEvent;
    public UnityEvent action;
    public void OnEventRaised()
    {
        action.Invoke();
    }
    private void OnEnable()
    {
        listenedEvent.RegisterListener(this);
    }
    private void OnDestroy()
    {
        listenedEvent.UnregisterListener(this);
    }
    
}
