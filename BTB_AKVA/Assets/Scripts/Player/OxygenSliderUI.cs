using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AKVA.Player
{
    public class OxygenSliderUI : MonoBehaviour
    {
        private Slider oxygenSlider;
        [SerializeField] private PlayerOxygen playerOxygen;

        private void Awake()
        {
            oxygenSlider = GetComponent<Slider>();
        }

        private void Update()
        {
            oxygenSlider.value = playerOxygen.GetOxygen();
        }
    }
}
