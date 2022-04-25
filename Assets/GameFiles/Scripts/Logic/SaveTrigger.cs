using GameFiles.Scripts.Services;
using GameFiles.Scripts.Services.SaveLoad;
using UnityEngine;

namespace Scripts.Logic
{
	public class SaveTrigger : MonoBehaviour
	{
		[SerializeField] private BoxCollider saveCollider;

		private ISaveLoadService _saveLoadService;

		private void Awake() =>
			_saveLoadService = AllServices.Container.Single<ISaveLoadService>();

		private void OnTriggerEnter(Collider other)
		{
			_saveLoadService.SaveProgress();
			Debug.Log("<color=green>Progress was save.</color>");
			gameObject.SetActive(false);
		}

		private void OnDrawGizmos()
		{
			if (saveCollider == null)
				return;

			Gizmos.color = new Color32(200, 0, 9, 130);
			Gizmos.DrawCube(transform.position + saveCollider.center, saveCollider.size);
		}
	}
}