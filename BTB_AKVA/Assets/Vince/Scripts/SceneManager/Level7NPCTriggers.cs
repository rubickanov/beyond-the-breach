using AKVA.Assets.Vince.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Level7NPCTriggers : MonoBehaviour
    {
        [SerializeField] GameObject[] scientists;
        [SerializeField] GameObject[] robots;
        public bool triggerScientist, triggerRobots;


        void TriggerScientist()
        {
            foreach(GameObject scientist in scientists)
            {
                scientist.SetActive(true);
            }
            this.enabled = false;
        }

        void TriggerRobots()
        {
            foreach(GameObject robot in robots)
            {
                RobotMovement robotMovement = robot.GetComponent<RobotMovement>();
                robotMovement.moveToNextLocation = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                if (triggerScientist)
                {
                    TriggerScientist();
                }else if (triggerRobots)
                {
                    TriggerRobots();
                    Destroy(gameObject);
                }
            }
        }
    }
}
