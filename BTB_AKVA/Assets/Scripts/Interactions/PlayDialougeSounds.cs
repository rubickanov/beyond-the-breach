using AKVA.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA
{
    public class PlayDialougeSounds : MonoBehaviour
    {
        [SerializeField] bool isRobot;
        [SerializeField] GameObject dialougeUI;
        [SerializeField] Transform wayPoint;
        Transform playerTransform;
        bool initSoundPlayed;
        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        void Update()
        {
            PlaySounds();
        }

        private void PlaySounds()
        {
            if (dialougeUI.activeSelf && Vector3.Distance(transform.position, playerTransform.position) <= 3f)
            {
                if (!initSoundPlayed)
                {
                    if(wayPoint != null) { MissionWayPoint.Instance.SetMarkerLocation(wayPoint); }

                    initSoundPlayed = true;
                    if(isRobot)
                    {
                        PlayRandomRobotSounds();
                    }
                    else
                    {
                        AudioManager.Instance.PlayOneShotAudio(AudioManager.Instance.sfxAudioSource, AudioManager.Instance.tabletSound);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (isRobot)
                    {
                        PlayRandomRobotSounds();

                    }
                    else
                    {
                        AudioManager.Instance.PlayOneShotAudio(AudioManager.Instance.sfxAudioSource, AudioManager.Instance.tabletSound);
                    }
                }
            }
            else
            {
                initSoundPlayed = false;
            }
        }

        void PlayRandomRobotSounds()
        {
            int randomIndex = Random.Range(0, AudioManager.Instance.robotSounds.Length);
            AudioManager.Instance.PlayOneShotAudio(AudioManager.Instance.sfxAudioSource, AudioManager.Instance.robotSounds[randomIndex]);
        }
    }
}
