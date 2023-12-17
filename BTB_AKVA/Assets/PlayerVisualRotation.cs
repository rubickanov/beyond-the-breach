using UnityEngine;

namespace AKVA.Player
{
    public class PlayerVisualRotation : MonoBehaviour
    {
        [SerializeField] private Transform orientation;

        private void Update()
        {
            transform.forward = orientation.forward;
        }
    }
}
