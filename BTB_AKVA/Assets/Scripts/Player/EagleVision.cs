using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;


namespace AKVA.Player
{
    public class EagleVision : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        private const int defaultRendererIndex = 0;
        private const int eagleRendererIndex = 1;

        private Renderer cameraRenderer;
        private UniversalAdditionalCameraData data;
        public bool isEagleVision;
        public bool qteActivate = true;

        [Header("UI")]
        public GameObject enableEagleVision;

        [Header("SFX")]
        public UnityEvent OnEagleVision;
        bool isPlaying;


        [SerializeField] private GameObject eagleVisionPostProcessing;
        
            
        private void Awake()
        {
            data = cam.GetComponent<UniversalAdditionalCameraData>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                PlayEagleVisionSfx();
                enableEagleVision.SetActive(true);
                isEagleVision = true;
            }
            else if(!qteActivate)
            {
                enableEagleVision.SetActive(false);
                isEagleVision = false;
            }

            if (isEagleVision)
            {
                EnableEagleVision();
            }
            else
            {
                isPlaying = false;
                DisableEagleVision();
            }
        }

        private void DisableEagleVision()
        {
            eagleVisionPostProcessing.SetActive(false);
            data.SetRenderer(defaultRendererIndex);
            data.antialiasing = AntialiasingMode.None;
        }

        private void EnableEagleVision()
        {
            eagleVisionPostProcessing.SetActive(true);
            data.SetRenderer(eagleRendererIndex);
            data.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
        }

        void PlayEagleVisionSfx()
        {
            if (!isPlaying)
            {
                isPlaying = true;
                OnEagleVision.Invoke();
            }
        }
    }
}
