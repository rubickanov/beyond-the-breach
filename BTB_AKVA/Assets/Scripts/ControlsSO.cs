using UnityEngine;


[CreateAssetMenu()]
public class ControlsSO : ScriptableObject
{
    [SerializeField] KeyCode forward;
    [SerializeField] KeyCode back;
    [SerializeField] KeyCode right;
    [SerializeField] KeyCode left;
}
