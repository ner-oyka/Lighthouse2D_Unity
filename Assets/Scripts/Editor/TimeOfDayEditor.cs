using UnityEditor;

[CustomEditor(typeof(TimeOfDay))]
public class TimeOfDayEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TimeOfDay timeOfDay = (TimeOfDay)target;
        timeOfDay.UpdateTime();
    }
}
