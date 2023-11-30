using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA
{
    public class ConveyorTest : MonoBehaviour
    {
        public float conveyorSpeed = 2f;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("PlayerEntered");
                collision.gameObject.transform.position += Vector3.forward * conveyorSpeed * Time.deltaTime;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                Debug.Log("PlayerEntered");
                other.gameObject.transform.position += Vector3.forward * conveyorSpeed * Time.deltaTime;
            }
        }
    }
}
