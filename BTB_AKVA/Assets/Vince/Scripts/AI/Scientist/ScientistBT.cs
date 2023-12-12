using AKVA.Assets.Vince.Scripts.Astar;
using AKVA.Vince.SO;
using Codice.Client.BaseCommands.BranchExplorer;
using log4net.Appender;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class ScientistBT : BTree
    {
        [SerializeField] public BoolReference playerDied;

        [Header("Mind Controlled")]
        public bool isMindControlled;
        public GameObject electricityVfx;

        [Header("Movement")]
        [SerializeField] Transform[] wayPoints;
        [SerializeField] bool enablePatrol;

        [Header("Interaction")]
        [SerializeField] Transform visionPos;
        [SerializeField] public float visionRadius;
        [SerializeField] LayerMask interactableObjLayer;
        public bool interacting;

        [Header("Body Scanning")]
        [SerializeField] LayerMask bodyScannerLayer;

        [Header("PlayerDetection")]
        public Transform player;
        public float timeToNoticePlayer = 5f;
        public Transform eyesPos;
        public LayerMask playerLayer;
        public LayerMask obstructionMask;
        public float angleToDetect = 80;
        [HideInInspector] public float initAngleToDetect;

        [Header("Object to guard")]
        public BoolReference objStatus;

        private void Start()
        {
            player = GameObject.Find("NewPlayer").transform;
            initAngleToDetect = angleToDetect;
            SetSwitchStatus();
        }

        protected override TreeNode SetupTree()
        {
            TreeNode root = new BTSelector(new List<TreeNode>{

                new BTMindControlled(transform, isMindControlled),
                new BTSequence(new List<TreeNode>
                {
                    new BTPlayerInRange(transform, visionPos, visionRadius, timeToNoticePlayer, eyesPos, playerLayer, obstructionMask),
                    new BTKillPlayer(transform, playerDied),
                }),
                new BTSequence(new List<TreeNode>
                {
                    new BTPowerIsOff(transform, objStatus),
                    new BTGoToPower(transform, wayPoints),
                }),
                new BTScanning(transform, visionRadius, bodyScannerLayer),
                new BTSequence(new List<TreeNode>
                {
                    new BTInteractableObjInRange(transform, visionPos, visionRadius, interactableObjLayer),
                    new BTInteractingObj(transform),
                }),
                new BTPatrol(transform, wayPoints, objStatus, enablePatrol),
            });
            return root;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(visionPos.position, visionRadius);
        }

        void SetSwitchStatus()
        {
            if (objStatus != null)
            {
                objStatus.value = true;
            }
        }

        public void SetMindControl(bool enable)
        {
            SetAlertSliderUI(enable);
            isMindControlled = enable;
        }

        void SetAlertSliderUI(bool enable)
        {
            gameObject.GetComponent<AlertSlider>().SetSlider(false);
            gameObject.GetComponent<AlertSlider>().UpdateSliderValue(0);
        }
    }
}
