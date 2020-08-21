using UnityEngine;
using UnityEditor;
using UniFlare;

[CustomEditor(typeof(UniFlareController))]
public class UniFlareControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var t = target as UniFlareController;
        if (t == null) return;

        EditorGUILayout.Space();
        if (GUILayout.Button("Force Initialize"))
        {
            Undo.RegisterCompleteObjectUndo(t.GetRecordObjects(), "Force Initialize Flare");
            t.Initialize();
        }

        if (GUILayout.Button("Add Hue 0.1"))
        {
            foreach (var recordObject in t.GetRecordObjects())
            {
                var so = new SerializedObject(recordObject);
                var color = so.FindProperty("_color")?.colorValue;
                if (color == null) continue;
                so.FindProperty("_color").colorValue = color.Value.OffsetHue(0.1f);
                so.ApplyModifiedProperties();
            }
        }
        EditorApplication.QueuePlayerLoopUpdate();
    }
}
