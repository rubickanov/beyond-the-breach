using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class PrisonRobotManager : MonoBehaviour
    {
        [SerializeField] RuntimeList listOfPrisons;

        public void EnableAllPrisons()
        {
            foreach (GameObject robot in listOfPrisons.items)
            {
                RobotMovement robotMovement = robot.GetComponent<RobotMovement>();
                robotMovement.activateRobot = true;
            }
        }
    }
}
