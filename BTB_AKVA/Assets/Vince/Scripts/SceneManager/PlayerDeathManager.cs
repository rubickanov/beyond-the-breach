using AKVA.Player;
using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    private static PlayerDeathManager _instance;
    public static PlayerDeathManager instance { get { return _instance; } }

    [SerializeField] BoolReference playerDied;
    [SerializeField] GameObject DeathScreenUI;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (playerDied.value)
        {
            ShowDeathScreen(true);
        }
    }

    public void ShowDeathScreen(bool value)
    {
        DeathScreenUI.SetActive(value);
        PlayerInput.Instance.DisablePlayerInput();
    }
}

