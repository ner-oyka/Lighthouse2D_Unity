using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Trigger))]
public class TriggerEditor : Editor
{
    private GUILayoutOption[] delayFieldOptions;

    private void OnEnable()
    {
        delayFieldOptions = new GUILayoutOption[]
        {
            GUILayout.Width(48),
        };
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Trigger script = (Trigger)target;

        script.UseTimeout = EditorGUILayout.Toggle("Use Timeout", script.UseTimeout);

        if (script.UseTimeout)
        {
            script.Delay = EditorGUILayout.FloatField(script.Delay, delayFieldOptions);
        }
    }
}