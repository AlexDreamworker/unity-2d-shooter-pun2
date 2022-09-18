using UnityEngine;

namespace ShooterPun2D.pt1
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class BounceAnimation : MonoBehaviour
	{
		[SerializeField] private float _frequency = 4f;
		[SerializeField] private float _amplitude = 0.1f;
		[SerializeField] private bool _isRandomize;

		private float _originalY;
        private Rigidbody2D _rigidbody;
		private float _seed;

        private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_originalY = _rigidbody.transform.position.y;

			if (_isRandomize) 
			{
				_seed = Random.value * Mathf.PI * 2;
			}
		}

		private void Update()
		{
			var position = _rigidbody.position;
			position.y = _originalY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
			_rigidbody.MovePosition(position);
		}		
	}
}

