using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class BodyScanner : MonoBehaviour
    {
        [SerializeField] float scanInterval = 3f;
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Scientist")
            {
                ScanningProceedureForScientist(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag == "Scientist")
            {
                StopAllCoroutines();
            }
        }

        void ScanningProceedureForScientist(Transform scientist)
        {
            StartCoroutine(RotateScientist(scientist));
        }

        IEnumerator RotateScientist(Transform scientist)
        {
            while (true)
            {
                yield return new WaitForSeconds(scanInterval);
                scientist.Rotate(new Vector3(0f, 90f, 0f));
            }
        }
    }
}