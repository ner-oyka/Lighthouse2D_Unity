using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMove : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent Agent;

    private Vector2 nextWaypoint;
    private float angleDifference;
    private float targetAngle;
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        rotateSpeed = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.destination = target.position;
        if (Agent.hasPath)
        {
            if (nextWaypoint != (Vector2)Agent.path.corners[1])
            {
                StartCoroutine("RotateToWaypoints");
                nextWaypoint = Agent.path.corners[1];
            }
        }

    }

    IEnumerator RotateToWaypoints()
    {
        Vector2 targetVector = Agent.path.corners[1] - transform.position;
        angleDifference = Vector2.SignedAngle(transform.up, targetVector);
        targetAngle = transform.localEulerAngles.z + angleDifference;

        if (targetAngle >= 360) { targetAngle -= 360; }
        else if (targetAngle < 0) { targetAngle += 360; }

        while (transform.localEulerAngles.z < targetAngle - 0.1f || transform.localEulerAngles.z > targetAngle + 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
