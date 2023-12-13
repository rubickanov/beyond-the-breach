using AKVA.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("MENUS")]
    [SerializeField] private GameObject uiToEnableAfterClose;
    
    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI sensValueText;
    [SerializeField] private TextMeshProUGUI bgMusicValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;

    
    [Header("BUTTONS")] 
    [SerializeField] private Slider sensSlider;
    [SerializeField] private Slider bgMusicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle uiAnimationToggle;
    [SerializeField] private Button backButton;

    [Header("DATA")] 
    [SerializeField] private FloatReference mouseSens; 
    [SerializeField] private FloatReference bgMusicVolume; 
    [SerializeField] private FloatReference sfxVolume; 
    [SerializeField] private BooleanReference useUIAnimation; 

    private void Start()
    {
        SetupButtons();

        SetupSlider(sensSlider, mouseSens);
        SetupSlider(bgMusicSlider, bgMusicVolume);
        SetupSlider(sfxSlider, sfxVolume);
        
        SetupToggle(uiAnimationToggle, useUIAnimation);
        
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleSlider(sensSlider, sensValueText, mouseSens, "0.0");
        HandleSlider(bgMusicSlider, bgMusicValueText, bgMusicVolume, "0");
        HandleSlider(sfxSlider, sfxValueText, sfxVolume, "0");
        
        HandleToggle(uiAnimationToggle, useUIAnimation);
    }
    
    private void Close()
    {
        uiToEnableAfterClose.SetActive(true);
        this.gameObject.SetActive(false);
    }


    private void SetupButtons()
    {
        backButton.onClick.AddListener(Close);
    }

    private void SetupSlider(Slider slider, FloatReference floatReference)
    {
        slider.minValue = floatReference.minValue;
        slider.maxValue = floatReference.maxValue;

        slider.value = floatReference.value;
    }

    private void HandleSlider(Slider slider, TextMeshProUGUI floatValueText, FloatReference floatReference, string format)
    {
        floatValueText.text = slider.value.ToString(format);
        floatReference.value = slider.value;
    }

    private void SetupToggle(Toggle toggle, BooleanReference boolReference)
    {
        toggle.isOn = boolReference.value;
    }
    
    private void HandleToggle(Toggle toggle, BooleanReference boolReference)
    {
        boolReference.value = toggle.isOn;
    }
}
