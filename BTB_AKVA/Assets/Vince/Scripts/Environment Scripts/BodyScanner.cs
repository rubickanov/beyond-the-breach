using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class BodyScanner : MonoBehaviour
    {
        [SerializeField] float scanInterval = 3f;
        [SerializeField] GameObject scanLight;
        [SerializeField] Material outerLightsMat, scanLightMat, scannerObjMat;
        [SerializeField] Color scanFailed, scanSuccess;
        [SerializeField] GameObject scientist;
        bool[] successfulScan;
        public bool scientistEntered = true;
        public int rotateCount;
        Transform player;

        bool startPlayerScanTime;
        public float currentPlayerScanningTime;
        public float maxScanTime = 5;
        bool beginLightAnimation;
        bool playerEscaped;

        private void Start()
        {
            successfulScan = new bool[4];
            SetScannerOuterLightsColor(scanFailed);
            SetScannerRayLightColor(scanSuccess);
            outerLightsMat.SetColor("_EmissionColor", Color.red);
            scannerObjMat.SetColor("_EmissionColor", Color.white);
        }

        private void Update()
        {
            PlayerScanProcedure();
            PlayerScanTimer();
            if (playerEscaped)
            {
                ErrorLightAnimation();
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
                SetScannerOuterLightsColor(scanFailed);
            }

            if (other.tag == "Player")
            {
                if (player != null && !successfulScan[3])
                {
                    //player dead
                    SetScannerRayLightColor(scanFailed);
                    playerEscaped = true;
                }
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
            SetScannerOuterLightsColor(scanSuccess);
        }

        void PlayerScanProcedure()
        {
            if (player != null)
            {
                float playerRotationY = player.transform.rotation.eulerAngles.y;
                if (!successfulScan[0])
                {
                    StartCoroutine(BeginPlayerScan(3, 0));
                }

                if (scanLight.activeSelf && successfulScan[0] && !successfulScan[1] && currentPlayerScanningTime >= maxScanTime)
                {
                    if (playerRotationY < 200 && playerRotationY > 150)
                    {
                        StartCoroutine(BeginPlayerScan(3, 1));
                        scanLight.SetActive(false);
                    }
                    else
                    {
                        SetScannerRayLightColor(scanFailed);
                        ErrorLightAnimation();
                    }
                }
                else if (scanLight.activeSelf && successfulScan[1] && !successfulScan[2] && currentPlayerScanningTime >= maxScanTime)
                {
                    if (playerRotationY > 260 && playerRotationY < 300)
                    {
                        StartCoroutine(BeginPlayerScan(3, 2));
                        scanLight.SetActive(false);
                    }
                    else
                    {
                        SetScannerRayLightColor(scanFailed);
                        ErrorLightAnimation();
                    }
                }

                else if (scanLight.activeSelf && successfulScan[2] && !successfulScan[3] && currentPlayerScanningTime >= maxScanTime)
                {
                    if ((playerRotationY > 340 && playerRotationY < 360) || (playerRotationY >= 0 && playerRotationY < 40))
                    {
                        successfulScan[3] = true;
                        scanLight.SetActive(false);
                        ErrorLightAnimation();
                    }
                    else
                    {
                        SetScannerRayLightColor(scanFailed);
                    }
                }
                else if (successfulScan[3])
                {
                    SetScannerOuterLightsColor(scanSuccess);
                }
            }
        }

        private void PlayerScanTimer()
        {
            if (startPlayerScanTime)
            {
                if (currentPlayerScanningTime < maxScanTime)
                {
                    currentPlayerScanningTime += Time.deltaTime;
                }
                else
                {
                    currentPlayerScanningTime = 0;
                    startPlayerScanTime = false;
                }
            }
        }

        IEnumerator BeginPlayerScan(float activeDelay, int scanIndex)
        {
            yield return new WaitForSeconds(activeDelay);
            successfulScan[scanIndex] = true;
            startPlayerScanTime = true;
            scanLight.SetActive(true);
        }

        public void SetScannerRayLightColor(Color lightColor)
        {
            scanLightMat.SetColor("_TintColor", new Color(lightColor.r, lightColor.g, lightColor.b, 0.5f));
        }

        public void SetScannerOuterLightsColor(Color lightColor)
        {
            outerLightsMat.color = lightColor;
            outerLightsMat.SetColor("_EmissionColor", lightColor);
        }

        void ErrorLightAnimation()
        {
            if (!beginLightAnimation)
            {
                beginLightAnimation = true;
                StartCoroutine(AnimateScannerLight());
                scanLight.SetActive(false);
            }
        }

        IEnumerator AnimateScannerLight()
        {
            while (true)
            {
                scanLight.SetActive(false);
                outerLightsMat.SetColor("_EmissionColor", Color.black);
                scannerObjMat.SetColor("_EmissionColor", Color.black);
                yield return new WaitForSeconds(0.3f);
                scanLight.SetActive(true);
                outerLightsMat.SetColor("_EmissionColor", Color.red);
                scannerObjMat.SetColor("_EmissionColor", Color.white);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}