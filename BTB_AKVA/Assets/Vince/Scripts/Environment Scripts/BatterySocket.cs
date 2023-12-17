using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class BatterySocket : MonoBehaviour
    {
        [SerializeField] UnityEvent OnSocketEnabled;
        [SerializeField] UnityEvent OnSocketDisabled;
        [SerializeField] UnityEvent PlayEnableSound;

        [SerializeField] Transform batteryPlaceHolder;

        [Header("Colored Button")]
        [SerializeField] bool isColored;

        Material socketMaterial;

        public bool socketIsActive;
        Transform battery;

        public delegate void OnBatteryPlaced();
        public event OnBatteryPlaced onBatteryPlaced;

        public delegate void OnBatteryRemoved();
        public event OnBatteryRemoved onBatteryRemoved;

        bool sfxPlayed;

        private void Start()
        {
            socketMaterial = GetComponent<Renderer>().material;
            socketMaterial.DisableKeyword("_EMISSION");
        }

        private void Update()
        {
            if (battery != null)
            {
                InteractableBattery interactableBattery = battery.GetComponent<InteractableBattery>();

                if (interactableBattery != null && !interactableBattery.batteryOnHand)
                {
                    socketMaterial.EnableKeyword("_EMISSION");
                    OnSocketEnabled.Invoke();
                    battery.position = batteryPlaceHolder.position;
                    battery.rotation = batteryPlaceHolder.rotation;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Battery")
            {
                if (isColored && socketMaterial.name == collision.gameObject.GetComponent<Renderer>().material.name)
                {
                    PlayEnableSound.Invoke();
                    battery = collision.gameObject.transform;
                    onBatteryPlaced?.Invoke();
                    socketIsActive = true;
                }
                else if (!isColored)
                {
                    PlayEnableSound.Invoke();
                    battery = collision.gameObject.transform;
                    onBatteryPlaced?.Invoke();
                    socketIsActive = true;
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Battery")
            {
                onBatteryRemoved?.Invoke();
                OnSocketDisabled.Invoke();
                socketMaterial.DisableKeyword("_EMISSION");
                collision.gameObject.GetComponent<InteractableBattery>().inSocket = false;
                socketIsActive = false;
                battery = null;
            }
        }
    }
}