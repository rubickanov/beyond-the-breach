using UnityEngine;

namespace AKVA.Interaction
{
    public class ScannerMindControlHotFix : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Scientist")
            {
                other.GetComponent<MindControlledObject>().enabled = false;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Scientist")
            {
                other.GetComponent<MindControlledObject>().enabled = true;
            }
        }
    }
}
