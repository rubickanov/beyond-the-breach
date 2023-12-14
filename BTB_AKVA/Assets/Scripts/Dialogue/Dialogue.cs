using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AKVA.Player;

namespace AKVA.Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        public static Dialogue Instance { get; private set; }
    
        // Left Speaker
        [Space(10)]
        [SerializeField] private TextMeshProUGUI speakerOneName;
    
        // Right Speaker
        [Space(10)]
        [SerializeField] private TextMeshProUGUI speakerTwoName;
    
        [Space(10)]
        [SerializeField] private TextMeshProUGUI textField;

        [SerializeField] private float delayToEnableControls;
    
        private DialogueSO _currentDialogue;
        private int _currentSentenceIndex;
        private bool _isActiveAlready;
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            CloseDialogueWindow();
        }

        private void Update()
        {
            ShowDialogue();
            
            if(Input.GetMouseButtonDown(0))
            {
                Step();
            }
        }

        private void ShowDialogue()
        {
             if (_currentDialogue.Sentences[_currentSentenceIndex].IsFirstSpeaker)
            {
                ShowLeftSpeaker();
            }
            else
            {
                ShowRightSpeaker();
            }

            textField.text = _currentDialogue.Sentences[_currentSentenceIndex].sentenceText;
        }

        private void ShowLeftSpeaker()
        {
            speakerOneName.gameObject.SetActive(true);
        
            speakerTwoName.gameObject.SetActive(false);
        }

        private void ShowRightSpeaker()
        {
            speakerTwoName.gameObject.SetActive(true);
        
            speakerOneName.gameObject.SetActive(false);
        }
    
        public void StartDialogue(DialogueSO dialogueSO)
        {
            if(_isActiveAlready) return;

            _isActiveAlready = true;

            _currentDialogue = dialogueSO;

            AssignSpeakersData(dialogueSO);

            _currentSentenceIndex = 0;

            OpenDialogueWindow();
        }

        private void AssignSpeakersData(DialogueSO dialogueSO)
        {
            speakerOneName.text = dialogueSO.SpeakerOne.Name;

            speakerTwoName.text = dialogueSO.SpeakerTwo.Name;
        }

        private void Step()
        {
            if (_currentDialogue.Sentences.Length - 1 == _currentSentenceIndex)
            {
                CloseDialogueWindow();
            }
            else
            {
                _currentSentenceIndex++;
            }
        }

    
    
        private void OpenDialogueWindow()
        {
            // disable player controls
            PlayerInput.Instance.DisablePlayerInput();
            gameObject.SetActive(true);
        }
        private void CloseDialogueWindow()
        {
            // enable player controls with delay
            //GameInput.Instance.EnableControlsWithDelay(delayToEnableControls);
            PlayerInput.Instance.EnablePlayerInput();

            _isActiveAlready = false;

            gameObject.SetActive(false);
        }
    }
}
