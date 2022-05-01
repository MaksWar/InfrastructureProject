using UnityEngine;

namespace Scripts.Logic
{
	public class SunController : MonoBehaviour
	{
		[SerializeField, Range(-10f, 10f)] private float speedX;
		[SerializeField, Range(-10f, 10f)] private float speedY;

		private void Update() =>
			transform.Rotate(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
	}
}