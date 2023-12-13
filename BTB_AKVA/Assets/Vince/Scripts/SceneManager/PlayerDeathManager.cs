using AKVA.Player;
using AKVA.Vince.SO;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    private static PlayerDeathManager _instance;
    public static PlayerDeathManager Instance { get { return _instance; } }

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
            playerDied.value = false;
        }
    }

    private void Start()
    {
        DeathScreenUI.SetActive(false);
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DeathScreenUI.SetActive(value);
        PlayerInput.Instance.DisablePlayerInput();
    }

    public void ResetPlayerDied()
    {
        playerDied.value = false;
    }
}

