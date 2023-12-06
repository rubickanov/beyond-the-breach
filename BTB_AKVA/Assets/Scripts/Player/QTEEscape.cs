using System;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

namespace AKVA.Player
{
    public class QTEEscape : MonoBehaviour
    {
        private bool isActive;

        [SerializeField] private Slider slider;
        [SerializeField] private float maxSliderValue;
        [SerializeField] private float plusValuePerClick;
        [SerializeField] private float minusValuePerTick;
        private Rigidbody rb;

        [Header("CAMERA SHAKE")] 
        [SerializeField] private float magnitude;
        [SerializeField] private float roughness;
        [SerializeField] private float fadeInTime;
        [SerializeField] private float fadeOutTime;


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

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
                CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            }
            
            
            if (isActive)
            {
                PlayerInput.Instance.DisablePlayerInput();
            }
            else
            {
                PlayerInput.Instance.EnablePlayerInput();
            }

            rb.velocity = Vector3.zero;
            
        }

        private void Escape()
        {
            slider.value += plusValuePerClick;
            if (slider.value >= maxSliderValue)
            {
                isActive = false;
                CameraShaker.Instance.enabled = false;
                Cancel();
            }
        }

        private void DecreaseValuePerTick()
        {
            slider.value -= minusValuePerTick * Time.deltaTime;
        }

        public void Cancel()
        {
            Destroy(slider.transform.parent.gameObject);
            //CameraShaker.Instance.enabled = false;
            Destroy(this);
        }
    }
}
