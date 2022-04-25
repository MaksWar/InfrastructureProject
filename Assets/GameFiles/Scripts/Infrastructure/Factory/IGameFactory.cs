using System;
using System.Collections.Generic;
using GameFiles.Scripts.Services;
using GameFiles.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace GameFiles.Scripts.Infrastructure.Factory
{
	public interface IGameFactory : IService
	{
		List<ISavedProgressReader> ProgressReaders { get; }

		List<ISavedProgress> ProgressWriters { get; }

		GameObject HeroGameObject { get; }

		GameObject CreateHero(GameObject at);

		event Action HeroCreated;

		void CreateHUD();

		void CleanUp();
	}
}