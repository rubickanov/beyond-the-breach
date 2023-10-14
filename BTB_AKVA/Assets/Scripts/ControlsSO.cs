using UnityEngine;


[CreateAssetMenu()]
public class ControlsSO : ScriptableObject
{
    [SerializeField] public KeyCode forward;
    [SerializeField] public KeyCode backwards;
    [SerializeField] public KeyCode right;
    [SerializeField] public KeyCode left;

    [SerializeField] public KeyCode jump;
}
