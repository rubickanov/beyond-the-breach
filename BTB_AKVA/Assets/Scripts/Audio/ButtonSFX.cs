using UnityEngine;

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
