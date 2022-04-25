﻿using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(EnemyAnimator))]
	public class AnimateAlongAgent : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent agent;
		[SerializeField] private EnemyAnimator animator;

		private const float MinimalVelocity = 0.1f;

		private void Update()
		{
			if (ShouldMove())
				animator.Move(agent.velocity.magnitude);
			else
				animator.StopMoving();
		}

		private bool ShouldMove() =>
			agent.velocity.magnitude > MinimalVelocity && agent.remainingDistance > agent.radius;

		#region Editor

		private void OnValidate()
		{
			if (agent == null)
				agent = GetComponent<NavMeshAgent>();
			if (animator == null)
				animator = GetComponent<EnemyAnimator>();
		}

		#endregion
	}
}