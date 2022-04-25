using GameFiles.Scripts.Services.SaveLoad;
using Scripts.Data;
using Scripts.Services.PersistentProgress;

namespace GameFiles.Scripts.Infrastructure.States
{
	public class LoadProgressState : IState
	{
		private readonly IPersistentProgressService _progressService;
		private readonly GameStateMachine _gameStateMachine;
		private readonly ISaveLoadService _saveLoadService;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
			ISaveLoadService saveLoadService)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadService = saveLoadService;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();
			_gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
		}

		public void Exit()
		{
		}

		private void LoadProgressOrInitNew() =>
			_progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

		private PlayerProgress NewProgress() =>
			new PlayerProgress(initialLevel: "MainScene");
	}
}