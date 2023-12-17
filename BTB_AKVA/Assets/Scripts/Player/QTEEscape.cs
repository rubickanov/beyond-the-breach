using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using System.Collections;
using UnityEngine.Serialization;

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
        public GameObject gameIntro;
        [FormerlySerializedAs("qte")] public GameObject qteSlider;

        [Header("QTE Sound")]
        public AudioSource sfxAudioSource;
        public AudioClip glitchSound;
        public AudioClip breakSound;
        float maxTimeToDisableSound = 1f;
        float currentSoundTime;
        bool soundIsPlaying;
        bool pressing;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            eagleVision = GetComponent<EagleVision>();

            if(playerRedHUD == null ){ return; }
            playerRedHUD?.SetActive(true);
            playerBlueHUD?.SetActive(false);
            gridBarrierImg?.SetActive(false);
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
                pressing = true;
                currentSoundTime = 0f;

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

            QTEGlitchSound();
        }

        private void Escape()
        {
            slider.value += plusValuePerClick;
            if (slider.value >= maxSliderValue)
            {
                isActive = false;
                DisableQTE();
                StartCoroutine(EnableEagleVisionForCoupleOfSeconds());
            }
        }

        private void DecreaseValuePerTick()
        {
            slider.value -= minusValuePerTick * Time.deltaTime;
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
            sfxAudioSource.Stop();
            sfxAudioSource.volume = .2f;
            sfxAudioSource.PlayOneShot(breakSound);
            playerBlueHUD.SetActive(true);
            playerRedHUD.SetActive(false);
            gameIntro.SetActive(false);
            eagleVision.isEagleVision = true;
            yield return new WaitForSeconds(eagleVisionTimeAfterQTE);
            eagleVision.isEagleVision = false;
            eagleVision.qteActivate = false;
            //CameraShaker.Instance.enabled = false;
            Destroy(this);
        }

        public void DisableQTE()
        {
            gameIntro.SetActive(false);
            qteSlider.SetActive(false);
            playerBlueHUD.SetActive(true);
            if (CameraShaker.Instance)
            {
                CameraShaker.Instance.enabled = false;
            }
            this.enabled = false;
        }

        public void DisableUI()
        {
            playerBlueHUD.SetActive(true);
            gameIntro.SetActive(false);
            qteSlider.SetActive(false);
        }

        void QTEGlitchSound()
        {
            if (pressing)
            {
                if(!soundIsPlaying)
                {
                    soundIsPlaying = true;
                    sfxAudioSource.PlayOneShot(glitchSound);
                }
                if(currentSoundTime < maxTimeToDisableSound)
                {
                    currentSoundTime += Time.deltaTime;
                }
                else
                {
                    pressing = false;
                    sfxAudioSource.Stop();
                    soundIsPlaying = false;
                }
            }
        }  
    }
}