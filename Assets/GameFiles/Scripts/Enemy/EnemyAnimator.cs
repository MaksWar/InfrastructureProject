using System;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Enemy
{
	public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
	{
		[SerializeField] private Animator animator;
		[SerializeField] private CharacterController characterController;

		private static readonly int Speed = Animator.StringToHash("Speed");
		private static readonly int IsMoving = Animator.StringToHash("IsMoving");
		private static readonly int Attack = Animator.StringToHash("Attacking");
		private static readonly int Die = Animator.StringToHash("Die");
		private static readonly int Victory = Animator.StringToHash("Victory");

		private readonly int _idleStateHash = Animator.StringToHash("Idle");
		private readonly int _attackStateHash = Animator.StringToHash("Attack");
		private readonly int _deathStateHash = Animator.StringToHash("Dead");
		private readonly int _walkingStateHash = Animator.StringToHash("Walk");
		private readonly int _victoryStateHash = Animator.StringToHash("Victory");

		public event Action<AnimatorState> OnStateEntered;
		public event Action<AnimatorState> OnStateExited;

		public AnimatorState State { get; private set; }

		public void EnteredState(int stateHash)
		{
			State = StateFor(stateHash);
			OnStateEntered?.Invoke(State);
		}

		public void ExitedState(int stateHash)
			=> OnStateExited?.Invoke(State);

		public void Move(float speed)
		{
			animator.SetBool(IsMoving, true);
			animator.SetFloat(Speed, speed);
		}

		public void StopMoving() => animator.SetBool(IsMoving, false);

		public void PlayAttack() => animator.SetTrigger(Attack);

		public void PlayDeath() => animator.SetTrigger(Die);

		public void PlayVictory() => animator.SetTrigger(Victory);

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
			else if (stateHash == _victoryStateHash)
				state = AnimatorState.Victory;
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