using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPun2D
{
	//[RequireComponent(typeof(Rigidbody2D))]
	//[RequireComponent(typeof(Collider2D))]
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private Transform _bodyGraphics;
		[SerializeField] private LayerCheckComponent _groundCheck;
		[SerializeField] private float _speed = 8f;
		[SerializeField] private float _jumpSpeed = 500f;

		private Rigidbody2D _rigidbody;
		private Collider2D _collider;
		private PhysicsMaterial2D _playerMaterial;
		private PhotonView _photonView;

		private float _xVelocity;
		private float _yVelocity;
		private Vector2 _direction;
		private bool _isGrounded;
		private bool _isJumping;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_collider = GetComponent<Collider2D>();
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			if (!_photonView.IsMine)
				Destroy(_rigidbody);
		}

		private void Update()
		{
			if (!_photonView.IsMine)
				return;

			_isGrounded = IsGrounded();
		}

		private void FixedUpdate()
		{
			if (!_photonView.IsMine)
				return;

			UpdateMovement();
			UpdateSpriteDirection();
		}

		public void SetDirection(Vector2 direction) //* CALL
		{
			_direction = direction;
		}

		private void UpdateMovement() 
		{
			var xVelocity = _direction.x * _speed;
			var yVelocity = CalculateYVelocity();

			_rigidbody.velocity = new Vector2(xVelocity, yVelocity);
		}

		private float CalculateYVelocity()
		{
			var yVelocity = _rigidbody.velocity.y;
			var isJumpPressing = _direction.y > 0;
			
			if (_isGrounded) 
			{
				_isJumping = false;
			}

			if (isJumpPressing) 
			{
				_isJumping = true;
				yVelocity = CalculateJumpVelocity(yVelocity);
			}
			else if (_rigidbody.velocity.y > 0 && _isJumping) 
			{
				yVelocity *= 0.5f;
			}

			return yVelocity;
		}

		private float CalculateJumpVelocity(float yVelocity) 
		{
			var isFalling = _rigidbody.velocity.y <= 0.001f;
			if (!isFalling) return yVelocity;

			if (_isGrounded)
			{
				yVelocity += _jumpSpeed;
			}

			return yVelocity;
		}

		private void UpdateSpriteDirection() 
		{
			if (_direction.x > 0) 
			{
				_bodyGraphics.transform.localScale = new Vector3(1, 1, 1);
			}
			else if (_direction.x < 0) 
			{
				_bodyGraphics.transform.localScale = new Vector3(-1, 1, 1);
			}
		}

		private bool IsGrounded() 
		{
			return _groundCheck.IsTouchingLayer;
		}			
	}
}

