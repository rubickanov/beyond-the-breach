using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class AlertSlider : MonoBehaviour
    {
        [SerializeField] GameObject alertSliderObj;
        ScientistBT sciBT;
        Slider slider;
        private void Start()
        {
            slider = alertSliderObj.GetComponent<Slider>();
            sciBT = GetComponent<ScientistBT>();
            InitSlider();
        }

        void InitSlider()
        {
            slider.maxValue = sciBT.timeToNoticePlayer;
            slider.value = 0;
        }

        public void SetSlider(bool enable)
        {
            alertSliderObj.SetActive(enable);
        }

        public void UpdateSliderValue(float value)
        {
            alertSliderObj.transform.LookAt(Camera.main.transform);
            slider.value = value;
        }
    }
}
