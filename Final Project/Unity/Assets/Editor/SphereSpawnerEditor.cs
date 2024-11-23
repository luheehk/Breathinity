using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SphereSpawner))]
public class SphereSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SphereSpawner spawner = (SphereSpawner)target;

        DrawDefaultInspector();

        spawner.inputValue = EditorGUILayout.Slider("Input Value", spawner.inputValue, 0f, 1f);

        if (GUI.changed)
        {
            spawner.UpdateSpheres();
        }
    }
}
