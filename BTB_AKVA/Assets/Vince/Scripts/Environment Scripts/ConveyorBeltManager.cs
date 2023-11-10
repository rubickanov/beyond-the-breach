using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class ConveyorBeltManager : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;
        [SerializeField] GameObject[] conveyors;
        [SerializeField] Material disabledMat;
        [SerializeField] bool disable;
        [SerializeField] Material initMat;
        void Start()
        {
            initMat = conveyors[0].GetComponent<Renderer>().material;
        }

        public void ConveyorBeltEnabled(bool enable)
        {
            if (enable)
            {
                foreach (GameObject conveyor in conveyors)
                {
                    conveyor.GetComponent<Renderer>().material = initMat;
                    MovingPlatform movingPlatform = conveyor.GetComponent<MovingPlatform>();
                    movingPlatform.pos2 = true;
                }
            }
            else
            {
                foreach (GameObject conveyor in conveyors)
                {
                    conveyor.GetComponent<Renderer>().material = disabledMat;
                    MovingPlatform movingPlatform = conveyor.GetComponent<MovingPlatform>();
                    movingPlatform.pos2 = false;
                }
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "MovingPlatform")
            {
                other.transform.position = spawnPoint.position;
            }
        }

        private void OnDrawGizmos()
        {
            if (spawnPoint == null) { return; }
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoint.position, 1f);
        }
    }
}