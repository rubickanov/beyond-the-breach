using UnityEngine;

namespace AKVA.Player
{
    public class CameraLayersCullDistances : MonoBehaviour
    {
        [SerializeField] private int[] layerMaskIndex;
        [SerializeField] private float distanceToNotRender;

        private float[] distances;
        private Camera cam;
        
        private void Start()
        {
            cam = GetComponent<Camera>();
            
            distances = new float[32];
            
            for (int i = 0; i < layerMaskIndex.Length; i++)
            {
                distances[layerMaskIndex[i]] = distanceToNotRender;
            }
            cam.layerCullDistances = distances;
        }
    }
}
