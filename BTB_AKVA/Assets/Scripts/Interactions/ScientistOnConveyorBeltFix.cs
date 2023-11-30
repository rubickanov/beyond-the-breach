using UnityEngine;

namespace AKVA.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class ScientistOnConveyorBeltFix : MonoBehaviour
    {
        private Rigidbody rb;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
