using System;
using UnityEngine;

namespace Scripts.CameraLogic
{
	public class CameraFollower : MonoBehaviour
	{
		[Header("Обьект следования")]
		[SerializeField] private Transform followPoint;

		[Header("Параметры")]
		[SerializeField] private float rotationAngleX;
		[SerializeField] private int distance;
		[SerializeField] private float offsetY;

		private void LateUpdate()
		{
			if (followPoint == null)
				return;

			var rotation = Quaternion.Euler(rotationAngleX, 0, 0);

			var position = rotation * new Vector3(0, 0, -distance) + FollowingPosition();

			transform.rotation = rotation;
			transform.position = position;
		}

		public void Follow(Transform following) =>
			followPoint = following;

		private Vector3 FollowingPosition()
		{
			var followingPosition = followPoint.position;
			followingPosition.y += offsetY;

			return followingPosition;
		}
	}
}