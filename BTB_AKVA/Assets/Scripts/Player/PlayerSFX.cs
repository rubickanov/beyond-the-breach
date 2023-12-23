using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Player  
{
    public class PlayerSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip footstepSound;
        [Range(0, 1)] [SerializeField] private float footStepVolume;

        public void OnFootstep()
        {
            AudioManager.Instance.SFX_PlaySoundAtPoint(footstepSound, transform.parent.position, footStepVolume);
        }
    }
}
