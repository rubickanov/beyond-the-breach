using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListeners : MonoBehaviour
{
    [NonReorderable]
    public GameEventListener[] gameEventListeners;
    private void OnEnable()
    {
        RegisterGameEventListeners();
    }

    private void RegisterGameEventListeners()
    {
        for (int i = 0; i < gameEventListeners.Length; i++)
        {
            gameEventListeners[i].RegisterListener(gameEventListeners[i]);
        }
    }

    private void UnRegisterGameEventListeners()
    {
        for (int i = 0; i < gameEventListeners.Length; i++)
        {
            gameEventListeners[i].UnRegisterListener(gameEventListeners[i]);
        }
    }
}
