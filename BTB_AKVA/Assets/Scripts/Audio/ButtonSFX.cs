using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AKVA
{
    public class ButtonSFX : MonoBehaviour
    {
        public AudioSource soundFX;
        public AudioClip hoverFX;
        public AudioClip pressedFX;

        public void HoverSound()
        {
            soundFX.PlayOneShot(hoverFX);
        }

        public void ClickSound()
        {
            soundFX.PlayOneShot(pressedFX);
        }
    }
}
