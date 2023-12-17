using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA
{
    public class CameraShake : MonoBehaviour
    {
        public float magnitude;
        public float roughness;
        public float fadeInTime;
        public float fadeOutTime;

        public void ShakeCamera()
        {
            CameraShaker.Instance.enabled = true;
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            StartCoroutine(DisableCameraShaker());
        }

        IEnumerator DisableCameraShaker()
        {
            yield return new WaitForSeconds(fadeOutTime);
            CameraShaker.Instance.enabled = false;
        }
    }
}
