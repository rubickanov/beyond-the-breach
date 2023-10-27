using UnityEngine;
using AKVA.Dialogue;

namespace AKVA.Interaction
{
    public class TestInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueSO dialogue;

        public void Interact()
        {
            Dialogue.Dialogue.Instance.StartDialogue(dialogue);
            Debug.Log("Start dialogue");
        }
    }
}
