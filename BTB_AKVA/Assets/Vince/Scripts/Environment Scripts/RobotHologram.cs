using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHologram : MonoBehaviour
{
    public float glitchDelayTime = 4f;
    public float rotateSpeed = 3f;
    public Color initBaseColor;
    public Material mat;
    int randomNum;
    void Start()
    {
        StartCoroutine(RandomGlitchEffect());
    }

    private void Update()
    {
        InfiniteRotation();
    }

    private void InfiniteRotation()
    {
        transform.Rotate(new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
    }

    IEnumerator RandomGlitchEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(glitchDelayTime);
            randomNum = Random.Range(0, 2);

            if(randomNum >= 1)
            {
                GlitchEffect(true);
            }
            else
            {
                GlitchEffect(false);
            }
        }
    }

    void GlitchEffect(bool enable)
    {
        if (enable)
        {
            mat.SetFloat("_Amount", 1);
            mat.SetFloat("_CutOutThresh", 0.043f);
            mat.SetColor("_BaseColor", Color.red);
        }
        else
        {
            mat.SetFloat("_Amount", 0);
            mat.SetFloat("_CutOutThresh", 0);
            mat.SetColor("_BaseColor", initBaseColor);

        }

    }


}
