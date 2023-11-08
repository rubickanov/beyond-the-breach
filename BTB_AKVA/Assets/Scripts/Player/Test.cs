using UnityEngine;

namespace AKVA.Player
{
    public class Test : MonoBehaviour
    {
        public CharacterController controller;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                controller.enabled = false;
            }
        }
    }
}
