using UnityEngine;
using UnityEngine.UI;

public class OxygenSlider : MonoBehaviour
{
    private Slider oxygenSlider;
    [SerializeField] private Oxygen oxygen;

    private void Awake()
    {
        oxygenSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        oxygenSlider.value = oxygen.GetOxygen();
    }
}
