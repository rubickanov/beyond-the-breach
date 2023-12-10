using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class PositionLights : MonoBehaviour
    {
        [SerializeField] Color OnEnterColor;
        [SerializeField] Color OnExitColor;
        [SerializeField] Renderer[] renderers;
        Transform currentObjectEntered;

        private void Start()
        {
            SetRendererColor(OnExitColor);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Robot" || other.tag == "Player")
            {
                if (currentObjectEntered == null)
                {
                    currentObjectEntered = other.transform;
                    SetRendererColor(OnEnterColor);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (currentObjectEntered != null && currentObjectEntered.tag == other.tag)
            {
                currentObjectEntered = null;
                SetRendererColor(OnExitColor);
            }
        }

        void SetRendererColor(Color color)
        {
            foreach (Renderer renderer in renderers)
            {
                Material mat = renderer.material;
                mat.color = color;
            }
        }
    }
}
