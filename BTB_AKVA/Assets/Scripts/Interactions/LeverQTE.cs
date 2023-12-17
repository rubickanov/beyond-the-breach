using AKVA.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] AudioClip gateForceOpening;
        bool disabled;

        //Sound Timer
        float maxTimeToResetSound = 2f;
        float currentSoundTime;
        bool pressing;
        bool soundIsPlaying;
        void OnEnable()
        {
            leverTransform.Rotate(new Vector3(-99, 0f, 0f));
        }
        void Update()
        {
            if (leverTransform.localEulerAngles.x > 280f && leverTransform.localEulerAngles.x < 360f)
            {
                leverTransform.Rotate(new Vector3(-0.1f, 0f, 0f));
            }
            PlayGateCreachingSound();
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
                if(Camera.main != null)
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
    }
}
