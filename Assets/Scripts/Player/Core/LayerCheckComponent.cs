using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class LayerCheckComponent : MonoBehaviour
	{
		[SerializeField] private LayerMask _groundLayer;
		private Collider2D _collider;

		public bool IsTouchingLayer;

		void Awake()
		{
			_collider = GetComponent<Collider2D>();
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
		}

		void OnTriggerExit2D(Collider2D other)
		{
			IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
		}
	}
}

