using UnityEngine;

namespace ShooterPun2D
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private float _speed = 8f;
		[SerializeField] private float _jumpForce = 500f;

		private Rigidbody2D _rigidbody;
		private Collider2D _collider;
		private float xVelocity;

		private PhysicsMaterial2D _playerMaterial;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_collider = GetComponent<Collider2D>();	

			CreatePhysicsMaterial();		
		}

		private void Update()
		{
			xVelocity = Input.GetAxis("Horizontal");
			_rigidbody.velocity = new Vector2(xVelocity * _speed, _rigidbody.velocity.y);

			if (Input.GetKeyDown(KeyCode.Space)) 
			{
				Jump();
			}
		}

		private void Jump() 
		{
			_rigidbody.AddForce(transform.up * _jumpForce);
		}

		private void CreatePhysicsMaterial() 
		{
			_playerMaterial = new PhysicsMaterial2D();
			_playerMaterial.friction = 0.0f;
			_playerMaterial.bounciness = 0.0f;

			_rigidbody.sharedMaterial = _playerMaterial;
			_collider.sharedMaterial = _playerMaterial;
		}
	}
}

