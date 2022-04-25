using GameFiles.Scripts.Services;
using GameFiles.Scripts.Services.PersistentProgress;
using Scripts.Data;
using Scripts.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Character
{
	[RequireComponent(typeof(CharacterController))]
	public class CharacterMove : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private CharacterController characterController;

		[Header("Параметры"), Space(10)] [SerializeField]
		private float movementSpeed;

		private IInputService _inputService;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void Update()
		{
			var movementVector = Vector3.zero;

			if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
			{
				movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
				movementVector.y = 0;
				movementVector.Normalize();

				transform.forward = movementVector;
			}

			movementVector += Physics.gravity;
			characterController.Move(movementSpeed * movementVector * Time.deltaTime);
		}

		public void UpdateProgress(PlayerProgress progress) =>
			progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

		public void LoadProgress(PlayerProgress progress)
		{
			if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
			{
				Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
				if (savedPosition != null)
					Warp(to: savedPosition);
			}
		}

		private void Warp(Vector3Data to)
		{
			characterController.enabled = false;
			transform.position = to.AsUnityVector().AddY(characterController.height);
			characterController.enabled = true;
		}

		private static string CurrentLevel() =>
			SceneManager.GetActiveScene().name;

		#region Editor

		private void OnValidate()
		{
			if (characterController == null)
				characterController = GetComponent<CharacterController>();
		}

		#endregion
	}
}