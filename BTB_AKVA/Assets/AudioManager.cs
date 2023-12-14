using AKVA.Player;
using UnityEngine;

namespace AKVA
{
    public class AudioManager : MonoBehaviour
    {
        [Header("AudioSources")]
        [SerializeField] private AudioSource backgroundAudioSource;
        [SerializeField] private AudioSource sfxAudioSouce;

        [Header("Data")] 
        [SerializeField] private FloatReference bgMusicVolume;
        [SerializeField] private FloatReference sfxSoundsVolume;


        private void Update()
        {
            backgroundAudioSource.volume = bgMusicVolume.value / 100;
            sfxAudioSouce.volume = sfxSoundsVolume.value / 100;
        }

    }
}
