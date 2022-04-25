using System;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Character
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(Animator))]
	public class CharacterAnimator : MonoBehaviour, IAnimationStateReader
	{
		[SerializeField] private Animator animator;
		[SerializeField] private CharacterController characterController;

		private static readonly int MoveHash = Animator.StringToHash("Walking");
		private static readonly int AttackHash = Animator.StringToHash("Attacking");
		private static readonly int DiedHash = Animator.StringToHash("Died");

		private readonly int _idleStateHash = Animator.StringToHash("Idle");
		private readonly int _attackStateHash = Animator.StringToHash("Attack");
		private readonly int _deathStateHash = Animator.StringToHash("Death 01");
		private readonly int _walkingStateHash = Animator.StringToHash("Walk");

		public event Action<AnimatorState> OnStateEntered;
		public event Action<AnimatorState> OnStateExited;

		public AnimatorState State { get; private set; }

		public bool IsAttacking => State == AnimatorState.Attack;

		private void Update()
		{
			animator.SetFloat(MoveHash, characterController.velocity.magnitude, 0.1f, Time.deltaTime);
		}

		public void EnteredState(int stateHash)
		{
			State = StateFor(stateHash);
			OnStateEntered?.Invoke(State);
		}

		public void ExitedState(int stateHash)
			=> OnStateExited?.Invoke(State);

		public void PlayAttack() => animator.SetTrigger(AttackHash);

		public void PlayDeath() => animator.SetTrigger(DiedHash);

		public void ResetToIdle() => animator.Play(_idleStateHash);

		private AnimatorState StateFor(int stateHash)
		{
			AnimatorState state;
			if (stateHash == _idleStateHash)
				state = AnimatorState.Idle;
			else if (stateHash == _attackStateHash)
				state = AnimatorState.Attack;
			else if (stateHash == _walkingStateHash)
				state = AnimatorState.Walking;
			else if (stateHash == _deathStateHash)
				state = AnimatorState.Died;
			else
				state = AnimatorState.Unknown;

			return state;
		}

		#region Editor

		private void OnValidate()
		{
			if (characterController == null)
				characterController = GetComponent<CharacterController>();
			if (animator == null)
				animator = GetComponent<Animator>();
		}

		#endregion
	}
}