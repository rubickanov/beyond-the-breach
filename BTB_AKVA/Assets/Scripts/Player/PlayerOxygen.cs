using UnityEngine;

namespace AKVA.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerOxygen : MonoBehaviour
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
            // if (playerMovement.IsMoving())
            // {
            //     oxygen -= Time.deltaTime;
            // }
        }

        public float GetOxygen()
        {
            return oxygen;
        }
    }
}
