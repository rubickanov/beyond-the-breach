using AKVA.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA
{
    public class PlayerDeathSfx : MonoBehaviour
    {
        [SerializeField] AudioSource sfxAudio;
        [SerializeField] AudioClip sfxClip;
        [SerializeField] GameObject deathScreen;
        [SerializeField] GameObject level7scientist;
        bool sfxEnabled;

        private void Update()
        {
            if(deathScreen.activeSelf && !sfxEnabled && !level7scientist.activeSelf)
            {
                sfxEnabled = true;
                sfxAudio.clip = sfxClip;
                sfxAudio.loop = true;
                sfxAudio.Play();
            }
        }
    }
}
