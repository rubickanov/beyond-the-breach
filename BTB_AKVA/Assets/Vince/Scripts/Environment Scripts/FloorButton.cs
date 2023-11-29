using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class FloorButton : MonoBehaviour
    {
        [SerializeField] Transform button;
        
        Vector3 initButtonPos;

        private void Start()
        {
            initButtonPos = button.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Player" &&  collision.gameObject.tag == "Scientist")
            {
                print("Detected");
                button.position -= new Vector3(0f, 1, 0f);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.tag == "Scientist")
            {
                button.position = initButtonPos;
            }
        }
    }
}