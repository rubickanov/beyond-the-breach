using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class SecurityComputer : MonoBehaviour
    {
        [SerializeField] RawImage[] rawImages;
        [SerializeField] bool computerIsActive = true;
        void Start()
        {
            SetVideo(computerIsActive);
        }

        public void SetVideo(bool enable)
        {
            if (!enable)
            {
                foreach(var image in rawImages)
                {
                    image.color = Color.black;
                }
            }
            else
            {
                foreach (var image in rawImages)
                {
                    image.color = Color.white;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
