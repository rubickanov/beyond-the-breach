using System;
using AKVA.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("MENUS")]
    [SerializeField] private GameObject uiToEnableAfterClose;


    [Header("BUTTONS")] 
    [SerializeField] private TextMeshProUGUI sensText;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private Button backButton;

    [Header("DATA")] 
    [SerializeField] private FloatReference mouseSens; 

    private void Start()
    {
        backButton.onClick.AddListener(Close);

        sensSlider.minValue = mouseSens.minValue;
        sensSlider.maxValue = mouseSens.maxValue;

        sensSlider.value = mouseSens.value;
        
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        sensText.text = sensSlider.value.ToString("0.0");
        mouseSens.value = sensSlider.value;
    }
    
    private void Close()
    {
        uiToEnableAfterClose.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
