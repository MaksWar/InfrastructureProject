using System.Collections.Generic;
using GameFiles.Scripts.Services;
using GameFiles.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace GameFiles.Scripts.Infrastructure.Factory
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(GameObject at);
		void CreateHUD();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		void CleanUp();
	}
}