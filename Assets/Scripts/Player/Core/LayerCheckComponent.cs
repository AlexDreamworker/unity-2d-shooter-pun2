using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class LayerCheckComponent : MonoBehaviour
	{
		[SerializeField] private LayerMask _groundLayer;
		private Collider2D _collider;

		public bool IsTouchingLayer;

		private void Awake()
		{
			_collider = GetComponent<Collider2D>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
		}
	}
}
