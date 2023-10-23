using UnityEngine;


[CreateAssetMenu()]
public class ControlsSO : ScriptableObject
{
    [SerializeField] public KeyCode forward;
    [SerializeField] public KeyCode back;
    [SerializeField] public KeyCode right;
    [SerializeField] public KeyCode left;
}
