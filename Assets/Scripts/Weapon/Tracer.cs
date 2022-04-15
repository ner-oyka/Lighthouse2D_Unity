using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer : MonoBehaviour
{
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 150.0f);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target - transform.position);

        if (Vector2.Distance(target, transform.position) < 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
