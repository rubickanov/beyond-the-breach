using UnityEngine;

namespace AKVA.Player
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform cameraPos;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, cameraPos.position, 0.5f);
        }
    }
}