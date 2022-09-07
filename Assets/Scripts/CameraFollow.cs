using UnityEngine;

namespace ShooterPun2D
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _offset = new Vector3(0, 0, -10f);
		[Range(1, 10)] [SerializeField] private float _smoothFactor = 3f;

		private void FixedUpdate()
		{
			Follow();
		}

		private void Follow() 
		{
			Vector3 targetPosition = _target.position + _offset;
			Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, _smoothFactor * Time.fixedDeltaTime);
			transform.position = smoothPosition;
		}
	}
}

