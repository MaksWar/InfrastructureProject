using GameFiles.Scripts.Infrastructure.Factory;
using GameFiles.Scripts.Services.PersistentProgress;
using Scripts.CameraLogic;
using Scripts.Infrastructure;
using Scripts.Logic;
using Scripts.Services.PersistentProgress;
using UnityEngine;

namespace GameFiles.Scripts.Infrastructure.States
{
	public class LoadLevelState : IPayLoadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";

		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
			IGameFactory gameFactory, IPersistentProgressService progressService)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_gameFactory.CleanUp();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();
			InformProgressReaders();

			_stateMachine.Enter<GameLoopState>();
		}

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
			{
				progressReader.LoadProgress(_progressService.Progress);
			}
		}

		private void InitGameWorld()
		{
			var character = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));
			_gameFactory.CreateHUD();

			CameraFollow(character);
		}

		private void CameraFollow(GameObject character)
		{
			Camera.main
				.GetComponent<CameraController>()
				.Follow(character.transform);
		}
	}
}