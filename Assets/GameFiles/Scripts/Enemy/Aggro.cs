using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
	public class Aggro : MonoBehaviour
	{
		[SerializeField] private TriggerObserver triggerObserver;
		[SerializeField] private Follow follow;

		[Header("Параметры"), Space(10)]
		[SerializeField] private float cooldown = 1f;

		private Coroutine _aggroCoroutine;
		private bool _hasAggroTarget;

		private void OnEnable()
		{
			triggerObserver.TriggerEnter += TriggerEnter;
			triggerObserver.TriggerExit += TriggerExit;
		}

		private void Start() =>
			SwitchFollowOff();

		private void OnDisable()
		{
			triggerObserver.TriggerEnter -= TriggerEnter;
			triggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerEnter(Collider obj)
		{
			if (_hasAggroTarget)
				return;

			_hasAggroTarget = true;

			StopAggroCoroutine();
			SwitchFollowOn();
		}

		private void TriggerExit(Collider obj)
		{
			if (_hasAggroTarget == false)
				return;

			_hasAggroTarget = false;
			_aggroCoroutine = StartCoroutine(SwitchOffFollowAfterCooldown());
		}

		private void SwitchFollowOn() =>
			follow.enabled = true;

		private void SwitchFollowOff() =>
			follow.enabled = false;

		private void StopAggroCoroutine()
		{
			if (_aggroCoroutine == null)
				return;

			StopCoroutine(_aggroCoroutine);
			_aggroCoroutine = null;
		}

		private IEnumerator SwitchOffFollowAfterCooldown()
		{
			yield return new WaitForSeconds(cooldown);

			SwitchFollowOff();
		}
	}
}