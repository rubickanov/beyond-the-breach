using AKVA.Player;
using UnityEditor;
using UnityEngine;

namespace AKVA
{
    public class AudioSFX : MonoBehaviour
    {
        private AudioSource audioSource;
        [SerializeField] private FloatReference sfxVolume;
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            sfxVolume = Resources.Load("SFX Volume") as FloatReference;
        }


        private void Update()
        {
            audioSource.volume = sfxVolume.value / 100;
        }
    }
}
