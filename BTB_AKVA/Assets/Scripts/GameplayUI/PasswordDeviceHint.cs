using UnityEngine;
using AKVA.Player;

public class PasswordDeviceHint : MonoBehaviour
{
    [SerializeField] private EagleVision eagleVision;
    [SerializeField] private GameObject worldCanvas;

    private void Update()
    {
        if(eagleVision.isEagleVision)
        {
            worldCanvas.SetActive(true);
        } else
        {
            worldCanvas.SetActive(false);
        }
    }
}
