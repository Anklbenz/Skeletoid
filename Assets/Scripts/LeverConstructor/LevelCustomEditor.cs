using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelEditor))]
public class LevelCustomEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var levelCreator = (LevelEditor)target;

        if (GUILayout.Button("Create"))
            levelCreator.CreatePrefab();

        if (GUILayout.Button("Clear"))
            levelCreator.Clear();

    }
}