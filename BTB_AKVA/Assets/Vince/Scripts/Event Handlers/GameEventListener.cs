using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventListener
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] UnityEvent response;

    /// <summary>
    /// Registering The listener to the game event
    /// </summary>
    /// <param name="gameEventListener"></param>
    public void RegisterListener(GameEventListener gameEventListener)
    {
        gameEvent.RegisterListener(gameEventListener);
    }

    public void UnRegisterListener(GameEventListener gameEventListener)
    {
        gameEvent.UnRegisterListener(gameEventListener);
    }
    public void OnEventRaise()
    {
        response.Invoke();
    }
}
