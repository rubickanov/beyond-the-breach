using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKVA.Vince.SO;
using Codice.Client.BaseCommands;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class BigMonitor : MonoBehaviour
    {
        [SerializeField] Sprite[] images;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Transform bg;
        [SerializeField] float showImgDelay = 3f;
        [SerializeField] BoolReference[] imagesAppeared;
        [SerializeField] BoolReference tvTurnedOn;
        float bgInitScale;
        float currentScale;
        private void Awake()
        {
            bgInitScale = bg.transform.localScale.y;
            bg.transform.localScale = new Vector3(bg.transform.localScale.x, 0f, bg.transform.localScale.z);
            spriteRenderer.enabled = false;
        }

        private void Update()
        {
            EnableTV();
            ShowImages();
        }

        private void ShowImages()
        {
            if (tvTurnedOn.value)
            {
                if (imagesAppeared[0].value)
                {
                    StartCoroutine(ShowImage(0));
                }

                if (imagesAppeared[1].value)
                {
                    StartCoroutine(ShowImage(1));
                }

                if (imagesAppeared[2].value)
                {
                    StartCoroutine(ShowImage(2));
                }
            }
        }

        private void EnableTV()
        {
            if (tvTurnedOn.value)
            {
                if (bg.transform.localScale.y < bgInitScale)
                {
                    float currentScale = Mathf.Lerp(bg.transform.localScale.y, bgInitScale, 6 * Time.deltaTime);
                    bg.transform.localScale = new Vector3(bg.transform.localScale.x, currentScale, bg.transform.localScale.z);
                }
            }
        }

        IEnumerator ShowImage(int imgNum)
        {
            yield return new WaitForSeconds(showImgDelay);

            spriteRenderer.enabled = true;
            spriteRenderer.sprite = images[imgNum];

            if (imgNum == 2)
            {
                spriteRenderer.transform.localScale = Vector3.one * 0.6609973f;
                bg.GetComponent<SpriteRenderer>().color = Color.red;
            }
            imagesAppeared[imgNum].value = false;
        }
    }
}
