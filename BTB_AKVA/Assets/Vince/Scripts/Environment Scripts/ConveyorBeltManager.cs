using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class ConveyorBeltManager : MonoBehaviour
    {
        [SerializeField] Material conveyorMat;
        [SerializeField] bool enableConveyor;
        [SerializeField] float conveyorSpeed = 0.5f;
        bool conveyorEnabled;
        private void Start()
        {
            conveyorMat.SetFloat("_AnimSpeed", conveyorSpeed);
        }
        public void EnableConveyorBelt(bool enable)
        {
            conveyorEnabled = enable;
            if (enable)
            {
                conveyorMat.SetFloat("_AnimSpeed", conveyorSpeed);
            }
            else
            {
                conveyorMat.SetFloat("_AnimSpeed", 0f);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (conveyorEnabled = true)
                {
                    collision.gameObject.transform.position += -Vector3.right * 1 * Time.deltaTime;
                }
                else
                {
                    return;
                }
            }
        }
    }
}