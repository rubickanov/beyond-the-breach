using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    public AudioClip[] robotSounds;
    public AudioClip tabletSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayOneShotAudio(AudioSource selectedAudioSource, AudioClip clipSoud)
    {
        selectedAudioSource.PlayOneShot(clipSoud);
    }
}
