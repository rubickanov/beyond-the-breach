using UnityEngine;

namespace AKVA.Player
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform cameraPos;

        private void LateUpdate()
        {
            transform.position = cameraPos.position;
        }
    }
}