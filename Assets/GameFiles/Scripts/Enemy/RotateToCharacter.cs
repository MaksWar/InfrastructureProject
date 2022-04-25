using GameFiles.Scripts.Infrastructure.Factory;
using GameFiles.Scripts.Services;
using UnityEngine;

namespace Scripts.Enemy
{
	public class RotateToCharacter : Follow
	{
		[SerializeField] private TriggerObserver triggerObserver;

		[Header("Параметры"), Space(10)]
		[SerializeField] private float speed;

		private IGameFactory _gameFactory;
		private Transform _character;
		private Vector3 _positionToLook;

		private void Start()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();

			if (CharacterExist())
				Initialize();
			else
				_gameFactory.HeroCreated += Initialize;
		}

		private void Update()
		{
			if (InitializedCharacter())
				LookAtCharacter();
		}

		private bool CharacterExist() =>
			_gameFactory.HeroGameObject != null;

		private void Initialize() =>
			_character = _gameFactory.HeroGameObject.transform;

		private void LookAtCharacter()
		{
			UpdatePositionToLook();

			transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
		}

		private void UpdatePositionToLook()
		{
			Vector3 positionDifference = _character.position - transform.position;
			_positionToLook = new Vector3(positionDifference.x, transform.position.y, positionDifference.z);
		}

		private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
			Quaternion.Lerp(rotation, LookRotation(positionToLook), SmoothSpeed());

		private static Quaternion LookRotation(Vector3 positionToLook) =>
			Quaternion.LookRotation(positionToLook);

		private float SmoothSpeed() =>
			speed * Time.deltaTime;

		private bool InitializedCharacter() =>
			_character != null;
	}
}