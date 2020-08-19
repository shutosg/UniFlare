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
            t.Initialize();
            Undo.RegisterCompleteObjectUndo(t.GetRecordObjects(), "Force Initialize Flare");
        }

        if (GUILayout.Button("Add Hue 0.1"))
        {
            Undo.RegisterCompleteObjectUndo(t.GetRecordObjects(), "Shift Flare Hue");
            t.ShiftColorHue(0.1f);
        }
        EditorApplication.QueuePlayerLoopUpdate();
    }
}
