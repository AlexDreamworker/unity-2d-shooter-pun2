using UnityEngine;

namespace ShooterPun2D
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private float _speed = 8f;
		[SerializeField] private float _jumpForce = 500f;
		[SerializeField] private Transform _bodyGraphics;

		private Rigidbody2D _rigidbody;
		private Collider2D _collider;
		private PhysicsMaterial2D _playerMaterial;

		//private PlayerWeapon _playerWeapon;

		//controller
		private float _xVelocity;
		private float _yVelocity;
		//private Vector2 _aimDirection;	

		private Vector2 _direction;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_collider = GetComponent<Collider2D>();

			//_playerWeapon = GetComponent<PlayerWeapon>();
		}

		private void Update()
		{
			//FaceMouse();

			//_xVelocity = GetComponent<PlayerInputReader>().XVelocity;
			//_aimDirection = GetComponent<PlayerInputReader>().AimDirection;
		}

		private void FixedUpdate()
		{			
			//_rigidbody.velocity = new Vector2(_xVelocity * _speed, _rigidbody.velocity.y);
			UpdateMovement();
			UpdateSpriteDirection();
		}

		public void SetDirection(Vector2 direction) //* CALL
		{
			_direction = direction;
		}

		private void UpdateMovement()
		{
			_xVelocity = _direction.x * _speed;
			_rigidbody.velocity = new Vector2(_xVelocity, _rigidbody.velocity.y);
		}

		private void UpdateSpriteDirection()
		{
			if (_xVelocity > 0)
			{
				_bodyGraphics.transform.localScale = new Vector3(1, 1, 1);
			}
			else
			{
				_bodyGraphics.transform.localScale = new Vector3(-1, 1, 1);
			}
		}

		// private void FaceMouse() 
		// {
		// 	_playerWeapon.WeaponHolder.transform.right = _aimDirection;
		// }		

		public void Jump() //* CALL
		{
			_rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, _direction.y * _jumpForce));
			//_rigidbody.AddForce(transform.up * _jumpForce);
		}				
	}
}

