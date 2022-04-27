using UnityEngine;

namespace GameFiles.Scripts.BuildsLogic
{
	public class ObjectRotator : MonoBehaviour
	{
		[SerializeField] private Transform target;

		[SerializeField] private float speed;

		private void Update()
		{
			target.Rotate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
		}
	}
}