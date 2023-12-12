using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] BoolReference playerDead;
    [SerializeField] Transform fire;

    public float maxExpandTime = .5f;
    float currentTime;
    bool fireExpanded;
    float targetSize = 5.5f;
    Vector3 fireInitScale;

    private void Start()
    {
        fireInitScale = fire.transform.localScale;
    }

    private void Update()
    {
        ExpandFire();
    }

    void ExpandFire()
    {
        if (fireExpanded)
        {
            if (currentTime < maxExpandTime)
            {
                // Calculate the interpolation factor based on the current time
                float t = currentTime / maxExpandTime;

                // Use Lerp to smoothly interpolate between the current scale and the target scale
                fire.localScale = Vector3.Lerp(fire.localScale, new Vector3(targetSize, targetSize, targetSize), t);

                currentTime += Time.deltaTime;
            }
            else
            {
                // Reset the scale to the initial scale
                fire.localScale = fireInitScale;
                currentTime = 0f;
                fireExpanded = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerDead.value = true;
        }

        if(other.tag == "Junk")
        {
            fireExpanded = true;
            Destroy(other.gameObject);
        }
    }
}
