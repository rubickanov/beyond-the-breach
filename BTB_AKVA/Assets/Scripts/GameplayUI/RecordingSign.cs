using UnityEngine;
using UnityEngine.Serialization;

public class RecordingSign : MonoBehaviour
{
    private float visibleTime = 2f;
    private float invisibleTime = 0.5f;
    private float timer;

    private bool isVisible;

    private SpriteRenderer[] renderers;
    [SerializeField]private float frequency;
    [SerializeField]private float magnitude;
    
    private void Start()
    {
        timer = visibleTime;

        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        // timer -= Time.deltaTime;
        // if (timer <= 0)
        // {
        //     isVisible = !isVisible;
        //
        //     timer = isVisible ? visibleTime : invisibleTime;
        // }
        //
        // if (isVisible)
        // {
        //     foreach (var renderer in renderers)
        //     {
        //         renderer.enabled = true;
        //     }       
        // }
        // else
        // {
        //     foreach (var renderer in renderers)
        //     {
        //         renderer.enabled = false;
        //     }  
        // }

        foreach (var renderer in renderers)
        {
            Color tempCol = renderer.color;
            tempCol.a = magnitude * Mathf.Sin(Time.time * frequency);
            renderer.color = tempCol;
        }
    }
}
