using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class CameraCircle : MonoBehaviour
    {
        [SerializeField] BoolReference playerDead;
        [SerializeField] BoolReference isMindControlling;
        [SerializeField] bool detectDuringMindControl;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !isMindControlling.value || other.tag == "Player" && detectDuringMindControl)
            {
                playerDead.value = true;
            }
        }
    }
}
