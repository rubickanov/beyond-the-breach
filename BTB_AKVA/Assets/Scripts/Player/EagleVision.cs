using UnityEngine;
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

        [SerializeField] private GameObject eagleVisionPostProcessing;
        
            
        private void Awake()
        {
            data = cam.GetComponent<UniversalAdditionalCameraData>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                enableEagleVision.SetActive(false);
                isEagleVision = true;
            }
            else if(Input.GetKeyUp(KeyCode.Tab) && !qteActivate)
            {
                enableEagleVision.SetActive(true);
                isEagleVision = false;
            }

            if (isEagleVision)
            {
                EnableEagleVision();
            }
            else
            {
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
    }
}
