using UnityEngine;

namespace AKVA.Controls
{
    [CreateAssetMenu()]
    public class ControlsSO : ScriptableObject
    {
        [SerializeField] public KeyCode forward;
        [SerializeField] public KeyCode backwards;
        [SerializeField] public KeyCode right;
        [SerializeField] public KeyCode left;

        [SerializeField] public KeyCode jump;

        [SerializeField] public KeyCode interact;
        [SerializeField] public KeyCode pick;
        [SerializeField] public KeyCode mindControl;
    }
}
