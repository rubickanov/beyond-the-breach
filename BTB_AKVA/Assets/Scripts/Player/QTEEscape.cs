using UnityEngine;
using UnityEngine.UI;

namespace AKVA.Player
{
    public class QTEEscape : MonoBehaviour
    {
        private bool isActive;

        [SerializeField] private Slider slider;
        [SerializeField] private float maxSliderValue;
        [SerializeField] private float plusValuePerClick;
        [SerializeField] private float minusValuePerTick;

        private void Start()
        {
            slider.maxValue = maxSliderValue;
            isActive = true;
        }

        private void Update()
        {
            DecreaseValuePerTick();
            
            if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
            {
                Escape();
            }
            
            
            if (isActive)
            {
                PlayerInput.Instance.DisablePlayerInput();
            }
            else
            {
                PlayerInput.Instance.EnablePlayerInput();
            }
            
            
        }

        private void Escape()
        {
            slider.value += plusValuePerClick;
            if (slider.value >= maxSliderValue)
            {
                isActive = false;
                Destroy(slider.transform.parent.gameObject);
                Destroy(this);
            }
        }

        private void DecreaseValuePerTick()
        {
            slider.value -= minusValuePerTick * Time.deltaTime;
        }
    }
}
