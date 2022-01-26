using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [HideInInspector]
    public bool FirstSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        if (FirstSpawn)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            if (playerTransform != null)
            {
                playerTransform.position = transform.position;
                playerTransform.rotation = transform.rotation;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 2.0f);
    }
}
