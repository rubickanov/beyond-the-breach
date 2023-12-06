using AKVA.Player;
using UnityEditor;
using UnityEngine;

namespace AKVA
{
    [CustomEditor(typeof(SavedCheckpoint))]
    public class SavedCheckpointCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (SavedCheckpoint)target;

            if (GUILayout.Button("RESET CHECKPOINT", GUILayout.Height(40)))
            {
                script.ResetCheckpoint();
            }
        }
    }
}
