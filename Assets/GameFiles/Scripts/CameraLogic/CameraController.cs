using System;
using Cinemachine;
using GameFiles.Scripts.Infrastructure.Factory;
using GameFiles.Scripts.Services;
using UnityEngine;

namespace Scripts.CameraLogic
{
	[RequireComponent(typeof(Camera))]
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private CinemachineFreeLook cinemachineFreeLook;

		[SerializeField] private bool isActiveCursor;

		private void Awake()
		{
			if (isActiveCursor)
				DisableCursor();
		}

		public void Follow(Transform target)
		{
			cinemachineFreeLook.Follow = target;
			cinemachineFreeLook.LookAt = target;
		}

		private static void DisableCursor() =>
			Cursor.visible = false;
	}
}