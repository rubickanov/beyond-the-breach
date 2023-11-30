using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class BodyScanner : MonoBehaviour
    {
        [SerializeField] float scanInterval = 3f;
        [SerializeField] GameObject scanLight;
        [SerializeField] Material outerLightsMat;
        [SerializeField] Color scanFailed, scanSuccess;
        [SerializeField] GameObject scientist;
        [SerializeField] bool[] successfulScan;
        public bool scientistEntered = true;
        public int rotateCount;
        [SerializeField] Transform player;

        bool startTime;
        bool delay;

        public float currentTime;
        float maxTime = 5;


        private void Start()
        {
            SetScannerLightsColor(scanFailed);
        }

        private void Update()
        {
            PlayerScanProcedure();

            if (startTime)
            {
                if (currentTime < maxTime)
                {
                    delay = true;
                    currentTime += Time.deltaTime;
                }
                else
                {
                    delay = false;
                    currentTime = 0;
                    startTime = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Scientist")
            {
                scientist = other.gameObject;
                ScanningProceedureForScientist();
                scientistEntered = true;
            }

            if (other.tag == "Player")
            {
                print("Player");
                player = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Scientist")
            {
                rotateCount = 0;
                scientist = null;
                scientistEntered = false;
                SetScannerLightsColor(scanFailed);
            }
        }

        void ScanningProceedureForScientist()
        {
            StartCoroutine(RotateScientist());
        }

        IEnumerator RotateScientist()
        {
            while (rotateCount < 3 && scientist != null)
            {
                yield return new WaitForSeconds(1);
                scanLight.SetActive(true);
                yield return new WaitForSeconds(scanInterval);
                scanLight.SetActive(false);
                yield return new WaitForSeconds(2);
                if (rotateCount < 2)
                {
                    scientist.transform.Rotate(new Vector3(0f, 90f, 0f));
                }
                rotateCount++;
            }
            SetScannerLightsColor(scanSuccess);
        }

        void PlayerScanProcedure()
        {
            if (player != null)
            {
                if (!successfulScan[0])
                {
                    StartCoroutine(BeginPlayerScan(3, 3));
                    successfulScan[0] = true;
                }
                if (scanLight.activeSelf && successfulScan[0] && !successfulScan[1] && player.transform.rotation.eulerAngles.y < 200 && player.transform.rotation.eulerAngles.y > 150)
                {
                    successfulScan[1] = true;
                    StartCoroutine(BeginPlayerScan(6, 3));
                }

                if (scanLight.activeSelf && successfulScan[1] && !successfulScan[2] && player.transform.rotation.eulerAngles.y > 260 && player.transform.rotation.eulerAngles.y < 300)
                {
                    StartCoroutine(BeginPlayerScan(8, 3));
                    successfulScan[2] = true;
                }
            
                if (scanLight.activeSelf && successfulScan[2] && !successfulScan[3])
                {
                    float playerRotationY = player.transform.rotation.eulerAngles.y;

                    // Check if the rotation is greater than 340 or less than 40
                    if ((playerRotationY > 340 && playerRotationY < 360) || (playerRotationY >= 0 && playerRotationY < 40))
                    {
                        StartCoroutine(BeginPlayerScan(8,3));
                        successfulScan[3] = true;
                    }
               
                }
                if (successfulScan[3])
                {
                    SetScannerLightsColor(scanSuccess);

                   
                }
                //else
                //{
                //    //Player dead
                //    print("Failed scan");
                //}

                //add fail sequence
            }
        }

        IEnumerator BeginPlayerScan(float activeDelay, float DisableDelay)
        {
            yield return new WaitForSeconds(activeDelay);
            scanLight.SetActive(true);
            yield return new WaitForSeconds(DisableDelay);
            scanLight.SetActive(false);
        }



        public void SetScannerLightsColor(Color lightColor)
        {
            outerLightsMat.color = lightColor;
            outerLightsMat.SetColor("_EmissionColor", lightColor);
        }
    }
}