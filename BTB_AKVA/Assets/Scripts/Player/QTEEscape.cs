using System;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEditor;
using System.Collections;
using UnityEditor.Graphs;
using UnityEngine.Experimental.Rendering;

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

        //Unlocking Eagle Vision

        [Header("Eagle Vision Event")]
        EagleVision eagleVision;

        public float maxEagleVisionTime = .3f;
        public float eagleVisionTimeAfterQTE = 3f;
        float currentTime;
        int randomIndex;
        bool eagleVisionEnabled;

        public GameObject gridBarrierImg;
        public GameObject playerBlueHUD;
        public GameObject playerRedHUD;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            eagleVision = GetComponent<EagleVision>();
            playerRedHUD.SetActive(true);
            playerBlueHUD.SetActive(false);
            gridBarrierImg.SetActive(false);
        }

        private void Start()
        {
            slider.maxValue = maxSliderValue;
            isActive = true;
        }

        private void Update()
        {
            EagleVisionTimer();

            DecreaseValuePerTick();

            if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
            {
                gridBarrierImg.SetActive(true);
                RandomEagleVision();
                Escape();
                CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            }else if (Input.GetKeyUp(PlayerInput.Instance.Controls.interact))
            {
                gridBarrierImg.SetActive(false);
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
            StartCoroutine(EnableEagleVisionForCoupleOfSeconds());
        }

        // eagle vision

        void EagleVisionTimer()
        {
            if (eagleVisionEnabled && isActive)
            {
                if (currentTime < maxEagleVisionTime)
                {
                    currentTime += Time.deltaTime;
                    eagleVision.isEagleVision = true;
                }
                else
                {
                    eagleVision.isEagleVision = false;
                    eagleVisionEnabled = false;
                    currentTime = 0;
                }
            }
        }

        void RandomEagleVision()
        {
            randomIndex = UnityEngine.Random.Range(0, 2);

            if (randomIndex >= 1)
            {
                eagleVisionEnabled = true;
            }
        }

        IEnumerator EnableEagleVisionForCoupleOfSeconds()
        {
            playerBlueHUD.SetActive(true);
            playerRedHUD.SetActive(false);
            eagleVision.isEagleVision = true;
            yield return new WaitForSeconds(eagleVisionTimeAfterQTE);
            eagleVision.qteActivate = false;
            eagleVision.isEagleVision = false;
            //CameraShaker.Instance.enabled = false;
            Destroy(this);
        }
    }
}