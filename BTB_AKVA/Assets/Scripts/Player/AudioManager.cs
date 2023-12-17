using AKVA;
using AKVA.Player;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    public AudioClip[] robotSounds;
    public AudioClip tabletSound;

    [SerializeField] private FloatReference musicVolume;
    [SerializeField] private FloatReference sfxVolume;
    
    
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

    private void Start()
    {
        GameObject[] allSFX = GameObject.FindGameObjectsWithTag("SFX");
        foreach (var sfxSoruce in allSFX)
        {
            sfxSoruce.AddComponent<AudioSFX>();
            
        }
    }

    public void PlayOneShotAudio(AudioSource selectedAudioSource, AudioClip clipSoud)
    {
        selectedAudioSource.PlayOneShot(clipSoud);
    }


    public void SFX_PlaySoundAtPoint(AudioClip audioClip, Vector3 point, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, point, (sfxVolume.value * volume) / 100);
    }
    
    
    
}
