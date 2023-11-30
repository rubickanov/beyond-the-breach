using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] Transform spawnPos;
        private void OnTriggerEnter(Collider other)
        {
            //if (other.tag == "Scientist")
            //{
            //    StartCoroutine(RelocateAI(other.transform));
            //}
        }

        IEnumerator RelocateAI(Transform ai)
        {
            yield return new WaitForSeconds(2);
            ai.position = spawnPos.position;
        }

    }
}