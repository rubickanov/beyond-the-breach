using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Controls;
using AKVA.Player;
using AKVA.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKVA.Assets.Vince.SO;
using AKVA.Vince.SO;
using TMPro;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.Events;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class SceneStateManager : MonoBehaviour
    {
        SceneState currentState;
        public TutorialIntroState tutorialIntro = new TutorialIntroState();
        public MovementTutorialState movementTutorial = new MovementTutorialState();
        public Room1State room1State = new Room1State();
        public Room2State room2State = new Room2State();
        public Room3State room3State = new Room3State();
        public PreMainSceneState preMainSceneState = new PreMainSceneState();

        public Transform playerTransform;
        [HideInInspector] public Picking playerPicking;
        public GameObject[] listOfAI;

        [Header("WayPoints")]
        public MissionWayPoint missionWayPoint;
        public Transform[] wayPoints;

        [Header("Game Intro")]
        public float initTxtFadeInTime = 0.5f;
        public float bgImageFadeOutTimeDelay = 8f;
        public float movementDelayTime = 13f;
        public TextMeshProUGUI initializeTxt;
        public Image blackBG;
        public GameObject PlayerHUDSprite, PlayerHUDWithoutAnim, neuroLabLogo, screenTxt, subtitleTxt;
        public Color hudColor;
        public UnityEvent OnLoad;
        public UnityEvent OnHUDActivate;
        public UnityEvent LabSpeaker;
        public UnityEvent OnSuccess;
        public UnityEvent MovementSuccess;
        public UnityEvent OnMovementEnabled;

        [Header("Movement Tutorial")]
        public TextMeshProUGUI movementTestTxt;
        public float timeDelayBeforePlayerMovement;
        public float timeDelayDuringTutorial;
        public DoubleDoor[] roomDoor;
        [HideInInspector] public TutorialScreen tutorialScreen;

        [Header("Room 1 Scene")]
        public DoubleDoor room1Door;
        public TutorialMonitor room1TutorialMonitor;
        public Transform room1PlayerPos, room1PlayerPos2;
        public RuntimeList room1Buttons;
        public RuntimeList room1Boxes;
        public RuntimeList room1PositionPoints;

        [Header("Room 2 Scene")]
        public Transform room2PlayerPos, room2PlayerPos2;
        public RuntimeList room2Boxes;
        public RuntimeList room2Buttons;
        public DoubleDoor room2Door;
        [HideInInspector] public Transform aiPos;

        [Header("Room 3 Scene")]
        public Transform room3PlayerPos;
        public GameObject[] neuroLabSprite;
        public GameObject[] interactableSprites;
        public TutorialMonitor[] room3TutorialMonitor;
        public BoolReference[] imagesAppeared;
        public BoolReference tvTurnedOn;
        public GameObject electricVFX;
        public Renderer room3Renderer;
        public Texture2D redTexture;
        public Light[] realTimeLights;
        public UnityEvent OnScreenCorrect;
        public UnityEvent OnRobotError;
        public UnityEvent ForceShutdownAudio;
        public UnityEvent AlarmSound;
        public UnityEvent HUDError;

        public AudioSource sfx;
        public AudioSource bgMusic;
        public AudioSource alarmSound;


        [Header("Pre-Main Scene")]
        public GameObject subtitle;
        public TextMeshProUGUI speakerTxt, subTxt;
        public float subtitleDelay;
        public UnityEvent Scientist1Sound;
        public UnityEvent Scientist2Sound;
        public UnityEvent MetalClangking;
        public UnityEvent ThrowingMetalSfx;

        [Header("Load To Next Scene")]
        public string sceneName;
        public float preMainSceneDelay = 3f;

        [Header("CAMERA SHAKE")]
        public CameraShaker cameraShaker;
        public float magnitude;
        public float roughness;
        public float fadeInTime;
        public float fadeOutTime;

        private void Awake()
        {
            //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            cameraShaker.enabled = false;
            playerPicking = playerTransform.GetComponent<Picking>();
            tutorialScreen = FindObjectOfType<TutorialScreen>();
            playerPicking.enabled = false;
            PlayerInput.Instance.DisablePlayerMovement();
            PlayerInput.Instance.DisablePlayerInput();
            SwitchState(tutorialIntro);
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.OnUpdateState(this);
            }
        }

        public void SwitchState(SceneState state)
        {
            currentState = state;
            state.OnEnterState(this);
        }
    }
}