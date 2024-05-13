using AKVA.Dialogue;
using UnityEngine;

namespace AKVA.Interaction
{
    public class NPCDialouge : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueSO dialogue;

        CodeGenerator codeGenerator => GetComponent<CodeGenerator>();
        
        void Start()
        {
            codeGenerator?.GenerateNewCodeForDialouge(dialogue.Sentences, dialogue);
        }

        public void Interact()
        {
            Dialogue.Dialogue.Instance.StartDialogue(dialogue);
        }
    }
}