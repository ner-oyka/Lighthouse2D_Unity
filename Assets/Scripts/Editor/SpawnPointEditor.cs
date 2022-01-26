using UnityEditor;

[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpawnPoint script = (SpawnPoint)target;

        script.FirstSpawn = EditorGUILayout.Toggle("First Spawn", script.FirstSpawn);

        if (script.FirstSpawn)
        {
            SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                spawnPoint.FirstSpawn = false;
            }
            script.FirstSpawn = !script.FirstSpawn;
        }        
    }
}