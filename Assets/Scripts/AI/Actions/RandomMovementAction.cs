using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{

	[Category("AI")]
	public class RandomMovementAction: ActionTask<NavMeshAgent>
	{
		Vector2 randomPos;

		protected override void OnExecute()
		{
			agent.updateRotation = false;
			agent.updateUpAxis = false;

			randomPos = (Vector2)agent.transform.position + Random.insideUnitCircle * 5.0f;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPos, out hit, 1.0f, NavMesh.AllAreas))
			{
				randomPos = hit.position;
			}
			else
            {
				EndAction(true);
			}
		}

		protected override void OnUpdate()
		{
			agent.destination = randomPos;

            if (agent.remainingDistance < 0.1)
			{
				EndAction(true);
			}			
		}
	}
}