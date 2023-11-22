using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


namespace AKVA.Player
{
    public class EagleVision : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        private const int defaultRendererIndex = 0;
        private const int eagleRendererIndex = 1;

        private Renderer cameraRenderer;
        private UniversalAdditionalCameraData data;
        private bool isEagleVision;
        
            
        private void Awake()
        {
            data = cam.GetComponent<UniversalAdditionalCameraData>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                isEagleVision = true;
            }
            else
            {
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
            data.SetRenderer(defaultRendererIndex);
            data.antialiasing = AntialiasingMode.None;
            data.renderPostProcessing = false;
        }

        private void EnableEagleVision()
        {
            data.SetRenderer(eagleRendererIndex);
            data.renderPostProcessing = true;
            data.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
        }
    }
}
