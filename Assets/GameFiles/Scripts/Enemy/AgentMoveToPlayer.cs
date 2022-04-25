using System;
using GameFiles.Scripts.Infrastructure.Factory;
using GameFiles.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class AgentMoveToPlayer : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent agent;

		private IGameFactory _gameFactory;
		private Transform _characterTransform;

		private const float MinDistance = 0.5f;

		private void Start()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();

			if (_gameFactory.HeroGameObject != null)
				InitializeCharacterTransform();
			else
				_gameFactory.HeroCreated += CharacterCreated;
		}

		private void Update()
		{
			if (InitializedCharacter() && CharacterNotReached())
				agent.destination = _characterTransform.position;
		}

		private void CharacterCreated()
		{
			InitializeCharacterTransform();
			_gameFactory.HeroCreated -= CharacterCreated;
		}

		private bool InitializedCharacter() =>
			_characterTransform != null;

		private void InitializeCharacterTransform() =>
			_characterTransform = _gameFactory.HeroGameObject.transform;

		private bool CharacterNotReached() =>
			Vector3.Distance(agent.transform.position, _characterTransform.position) >= MinDistance;

		#region Editor

		private void OnValidate()
		{
			if (agent == null)
				agent = GetComponent<NavMeshAgent>();
		}

		#endregion
	}
}