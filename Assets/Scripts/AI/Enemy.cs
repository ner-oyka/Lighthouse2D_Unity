using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyDataAsset;

    public BehaviourTree idleBehaviour;
    public BehaviourTree attackBehaviour;

    private BehaviourTreeOwner agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.AddComponent<BehaviourTreeOwner>();

        if (idleBehaviour != null)
        {
            agent.StartBehaviour(idleBehaviour);
        }
    }

    bool EnableAttackBehaviour()
    {
        if (attackBehaviour != null)
        {
            agent.SwitchBehaviour(attackBehaviour);
            return true;
        }
        return false;
    }

    bool EnableIdleBehaviour()
    {
        if (idleBehaviour != null)
        {
            agent.SwitchBehaviour(idleBehaviour);
            return true;
        }
        return false;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity; 
    }
}
