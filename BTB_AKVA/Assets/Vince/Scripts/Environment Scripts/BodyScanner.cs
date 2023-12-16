using AKVA.Vince.SO;
using Codice.CM.WorkspaceServer.Tree;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class BodyScanner : MonoBehaviour
    {
        [SerializeField] float scanInterval = 3f;
        [SerializeField] float scanningProcedureDelayTime = 10f;
        [SerializeField] GameObject scanLight;
        [SerializeField] Material outerLightsMat, scanLightMat, scannerObjMat;
        [SerializeField] Color scanFailed, scanSuccess;
        [SerializeField] GameObject scientist;
        bool[] successfulScan;
        public bool scientistEntered = true;
        public int rotateCount;
        [SerializeField] Transform player;
        [SerializeField] BoolReference playerIsDead;

        public UnityEvent OnEnterScan;
        public UnityEvent ScanInitSfx2;
        public UnityEvent OnScan;
        public UnityEvent OnScanSuccess;
        public UnityEvent OnCorrectRot;
        public UnityEvent OnScanFailed;

        bool startPlayerScanTime;
        public float currentPlayerScanningTime;
        public float maxScanTime = 5;
        bool beginLightAnimation;
        bool playerEscaped;
        bool startPlayerScan;
        bool playerEntered;
        bool error;
        bool [] initSFxPlayed;
        bool[] correctRotSfx;
        bool[] scanSfxIsPlaying; 

        private void Awake()
        {
            successfulScan = new bool[4];
            initSFxPlayed = new bool[2];
            correctRotSfx = new bool[4];
            scanSfxIsPlaying = new bool[4];

            SetScannerOuterLightsColor(scanFailed);
            SetScannerRayLightColor(scanSuccess);
            outerLightsMat.SetColor("_EmissionColor", Color.red);
            scannerObjMat.SetColor("_EmissionColor", Color.white);
        }

        private void Update()
        {
            if (player != null && !playerEntered)
            {
                playerEntered = true;
                StartCoroutine(StartPlayerScanDelay());
            }
            PlayerScanProcedure();
            PlayerScanTimer();
            if (playerEscaped)
            {
                error = true;
                ErrorLightAnimation();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Scientist")
            {
                scientist = other.gameObject;
                OnEnterScan.Invoke();
                SetSubtitle("Initializing scanning sequence. Please stand by for the required protocol.", scanningProcedureDelayTime);
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
                    playerIsDead.value = true;
                    SetScannerRayLightColor(scanFailed);
                    playerEscaped = true;
                }
            }
        }

        void ScanningProceedureForScientist()
        {
            StartCoroutine(RotateScientist());
        }

        void PlayerScanningProcedureDelay()
        {
            StartCoroutine(RotateScientist());
        }

        IEnumerator RotateScientist()
        {
            yield return new WaitForSeconds(scanningProcedureDelayTime);

            SetSubtitle("Ensure you face the specified direction once the green light activates, following protocol ID 0626.", 7f);
            ScanInitSfx2.Invoke();

            yield return new WaitForSeconds(7);

            while (rotateCount < 3 && scientist != null && !error)
            {
                yield return new WaitForSeconds(1);
                OnScan.Invoke();
                scanLight.SetActive(true);
                yield return new WaitForSeconds(scanInterval);
                OnCorrectRot.Invoke();
                scanLight.SetActive(false);
                yield return new WaitForSeconds(2);
                if (rotateCount < 2)
                {
                    scientist.transform.Rotate(new Vector3(0f, 90f, 0f));
                }
                rotateCount++;
            }
            scientist.transform.Rotate(new Vector3(0f, 180f, 0f));
            SetSubtitle("Scan complete. You may now proceed.", 3f);
            OnScanSuccess.Invoke();
            SetScannerOuterLightsColor(scanSuccess);
        }

        IEnumerator StartPlayerScanDelay()
        {
            if (!initSFxPlayed[0])
            {
                SetSubtitle("Initializing scanning sequence. Please stand by for the required protocol." , scanningProcedureDelayTime);
                OnEnterScan.Invoke();
                initSFxPlayed[0] = true;
            }
            yield return new WaitForSeconds(scanningProcedureDelayTime);

            if (!initSFxPlayed[1])
            {
                SetSubtitle("Ensure you face the specified direction once the green light activates, following protocol ID 0626.", 7f);
                initSFxPlayed[1] = true;
                ScanInitSfx2.Invoke();
            }

            yield return new WaitForSeconds(7);
            startPlayerScan = true;
        }

        void PlayerScanProcedure()
        {
            if (startPlayerScan)
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
                        PlayCorrectRotSfx(0);
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
                        PlayCorrectRotSfx(1);
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
                        PlayCorrectRotSfx(2);
                        SetSubtitle("Scan complete. You may now proceed.", 3f);
                        OnScanSuccess.Invoke();
                        successfulScan[3] = true;
                        scanLight.SetActive(false);
                    }
                    else
                    {
                        ErrorLightAnimation();
                        SetScannerRayLightColor(scanFailed);
                    }
                }
                else if (successfulScan[3])
                {
                    SetScannerOuterLightsColor(scanSuccess);
                    this.enabled = false;
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

            PlayScanSfx(scanIndex);
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
                SetSubtitle("Scan failed. Non-compliance detected. Shutdown protocol activated.", 12f);
                OnScanFailed.Invoke();
                playerIsDead.value = true;
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

        void PlayCorrectRotSfx(int boolIndex)
        {
            if (!correctRotSfx[boolIndex])
            {
                correctRotSfx[boolIndex] = true;
                OnCorrectRot.Invoke();
            }
        }

        void PlayScanSfx(int boolIndex)
        {
            if (!scanSfxIsPlaying[boolIndex])
            {
                scanSfxIsPlaying[boolIndex] = true;
                OnScan.Invoke();
            }
        }

        void SetSubtitle(string subTxt, float txtDuration)
        {
            SubtitleManager.Instance.ShowSubtitle("Scanning Machine:",subTxt, txtDuration);
        }
    }
}