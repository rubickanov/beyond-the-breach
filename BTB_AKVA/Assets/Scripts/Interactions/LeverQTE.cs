using AKVA.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AKVA.Interaction
{
    public class LeverQTE : MonoBehaviour, IInteractable
    {
        [SerializeField] UnityEvent OnLeverPushed;
        [SerializeField] Transform leverTransform;
        [SerializeField] GameObject finalLevelQTE;
        [SerializeField] GameObject playerFinalCamera;
        [SerializeField] float rotationForce = 0.1f;
        [SerializeField] AudioSource gateAudioSource;
        [SerializeField] AudioSource sfxAudioSource;
        [SerializeField] AudioSource musicAudioSource;
        [SerializeField] AudioClip endingMusic;
        [SerializeField] AudioClip gateForceOpening;
        [SerializeField] Image titleImage;
        [SerializeField] float fadeInLogoSpeed;
        float currentAlpha;
        float targetSfxVolume = 0.062f;
        bool disabled;
        bool sfxLowVolume;
        //Sound Timer
        float maxTimeToResetSound = 2f;
        float currentSoundTime;
        bool pressing;
        bool soundIsPlaying;
        void OnEnable()
        {
            EnableEndingMusic();
            leverTransform.Rotate(new Vector3(-99, 0f, 0f));
        }
        void Update()
        {
            if (leverTransform.localEulerAngles.x > 280f && leverTransform.localEulerAngles.x < 360f)
            {
                leverTransform.Rotate(new Vector3(-0.1f, 0f, 0f));
            }
            PlayGateCreachingSound();

            if (sfxLowVolume)
            {
                sfxAudioSource.volume = targetSfxVolume;
            }

        }
        public void ForceRotate()
        {
            OnLeverPushed.Invoke();
            leverTransform.Rotate(new Vector3(rotationForce, 0f, 0f));
        }


        public void Interact()
        {
            if (!disabled)
            {
                playerFinalCamera.SetActive(true);
                if (Camera.main != null)
                {
                    Camera.main.gameObject.SetActive(false);
                    finalLevelQTE.SetActive(true);
                }
            }
        }

        void PlayGateCreachingSound()
        {
            if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact) && finalLevelQTE.activeSelf)
            {
                currentSoundTime = 0;
                pressing = true;
            }

            if (pressing)
            {
                if (!soundIsPlaying)
                {
                    soundIsPlaying = true;
                    gateAudioSource.PlayOneShot(gateForceOpening);
                }
                if (currentSoundTime < maxTimeToResetSound)
                {
                    currentSoundTime += Time.deltaTime;
                }
                else
                {
                    pressing = false;
                    gateAudioSource.Stop();
                    soundIsPlaying = false;
                }
            }
        }

        public void DisableLever()
        {
            disabled = true;
        }

        void EnableEndingMusic()
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = endingMusic;
            musicAudioSource.Play();
            StartCoroutine(LowerSFXVolume());
        }

        IEnumerator LowerSFXVolume()
        {
            yield return new WaitForSeconds(4f);
            sfxLowVolume = true;
        }

        public void ShowGameTitle()
        {
            StartCoroutine(FadeInGameTitleLogo());
        }

        IEnumerator FadeInGameTitleLogo()
        {
            yield return new WaitForSeconds(5f);
            while(titleImage.color.a < 1)
            {
                yield return null;
                currentAlpha += Time.deltaTime * fadeInLogoSpeed;
                titleImage.color = new Color(titleImage.color.r, titleImage.color.b, titleImage.color.g, currentAlpha);
            }
        }
    }
}
