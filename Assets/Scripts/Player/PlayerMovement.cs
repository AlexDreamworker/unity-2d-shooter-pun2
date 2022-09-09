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

		private PlayerWeapon _playerWeapon;

		//controller
		private float _xVelocity;
		private Vector2 _aimDirection;	

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_collider = GetComponent<Collider2D>();

			_playerWeapon = GetComponent<PlayerWeapon>();
		}

		private void Update()
		{
			FaceMouse();

			_xVelocity = GetComponent<PlayerInput>().XVelocity;
			_aimDirection = GetComponent<PlayerInput>().AimDirection;
		}

		private void FixedUpdate()
		{			
			_rigidbody.velocity = new Vector2(_xVelocity * _speed, _rigidbody.velocity.y);
			UpdateSpriteDirection();
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

		private void FaceMouse() 
		{
			_playerWeapon.WeaponHolder.transform.right = _aimDirection;
		}		

		public void Jump() //* CALL
		{
			_rigidbody.AddForce(transform.up * _jumpForce);
		}				
	}
}

