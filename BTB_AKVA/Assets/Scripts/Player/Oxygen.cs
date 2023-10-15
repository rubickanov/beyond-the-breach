using UnityEngine;

namespace Player
{
    public class Oxygen : MonoBehaviour
    {
        [SerializeField] private float maxOxygen;
     
        private float oxygen;

        private PlayerMovement playerMovement;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            oxygen = maxOxygen;
        }

        private void Update()
        {
            if (playerMovement.IsMoving())
            {
                oxygen -= Time.deltaTime;
            }
        }

        public float GetOxygen()
        {
            return oxygen;
        }
    }
}
