using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class Teleport : MonoBehaviour
	{
		[SerializeField] private Transform _destinationPoint;

		void OnTriggerEnter2D(Collider2D other)
		{
			var target = other.gameObject.TryGetComponent(out PlayerControls player);
			
			if (target) 
				player.transform.position = _destinationPoint.position;
		}
	}
}

