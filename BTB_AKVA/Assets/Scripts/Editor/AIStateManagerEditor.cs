using UnityEngine;
using UnityEditor;

namespace AKVA.Assets.Vince.Scripts.AI
{
    //[CustomEditor(typeof(AIStateManager))]
    //public class AIStateManagerEditor : Editor
    //{
    //    AIStateManager stateManager;
    //    float handleSize = .3f;
    //    public override void OnInspectorGUI()
    //    {
    //        stateManager = (AIStateManager)target;
    //        base.OnInspectorGUI();
    //        AddSpawnPointButton();
    //        RemoveAPointButton();
    //    }

    //    //private void OnSceneGUI()
    //    //{
    //    //    DrawMoveHandlers();
    //    //}

    //    //void DrawMoveHandlers()
    //    //{
    //    //    Handles.color = Color.blue;
    //    //    if (stateManager.pathPoints.Count > 0)
    //    //    {
    //    //        for (int i = 0; i < stateManager.pathPoints.Count; i++)
    //    //        {
    //    //            Vector3 newPos = Handles.FreeMoveHandle(stateManager.pathPoints[i], handleSize, Vector3.zero, Handles.CylinderHandleCap);
    //    //            if (stateManager.pathPoints[i] != newPos)
    //    //            {
    //    //                Undo.RecordObject(stateManager, "MovePoint");
    //    //                stateManager.MoveSpawnPoint(i, newPos);
    //    //            }
    //    //        }
    //    //    }
    //    //}

    //    public void AddSpawnPointButton()
    //    {
    //        if (GUILayout.Button("Spawn A SpawnPoint"))
    //        {
    //            //if (stateManager.pathPoints.Count > 0)
    //            //{
    //            //    stateManager.pathPoints.Add(stateManager.pathPoints[stateManager.pathPoints.Count - 1]);
    //            //}
    //            //else
    //            //{
    //            //    stateManager.pathPoints.Add(stateManager.transform.position);
    //            //}
    //            GameObject spawnPoint = new GameObject();
    //            spawnPoint.name = $"{stateManager.gameObject.name}_PathPoint (1)";
    //            spawnPoint.transform.position = stateManager.gameObject.transform.position;


    //            stateManager.pathPoints.Add(spawnPoint.transform);
    //        }
    //    }

    //    void RemoveAPointButton()
    //    {
    //        if (GUILayout.Button("Remove A SpawnPoint"))
    //        {
    //            stateManager.pathPoints.Remove(stateManager.pathPoints[stateManager.pathPoints.Count - 1]);
    //        }
    //    }

    //    void RemoveAllSpawnPointsBtn()
    //    {
    //        if (GUILayout.Button("Remove All SpawnPoints"))
    //        {
    //            stateManager.pathPoints.Clear();
    //        }
    //    }
    //}
}
