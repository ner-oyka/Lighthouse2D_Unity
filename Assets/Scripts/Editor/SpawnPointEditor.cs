using UnityEditor;

[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpawnPoint script = (SpawnPoint)target;

        script.Entry = EditorGUILayout.Toggle("Entry", script.Entry);

        if (script.Entry)
        {
            SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                spawnPoint.Entry = false;
            }
            script.Entry = !script.Entry;
        }        
    }
}