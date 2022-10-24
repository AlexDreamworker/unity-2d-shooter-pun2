using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerControls : MonoBehaviour, IPunObservable
	{
		[Header("Reference")]
		[SerializeField] private float _speed;
		[SerializeField] private float _jumpSpeed = 500f;
		[SerializeField] private LayerCheckComponent _groundCheck;

		private Vector2 _moveDirection;
		private Vector2 _aimDirection;
		private bool _isGrounded;

		private PlayerBrain _playerBrain;

		void Awake()
		{
			_playerBrain = GetComponent<PlayerBrain>();
		}

		void Update()
		{
			_isGrounded = IsGrounded();
			_playerBrain.Graphics.UpdateSpriteDirectionLegs(_moveDirection.x);

			if (!_playerBrain.PhotonView.IsMine) 
				return;

			if (_aimDirection.x != 0 || _aimDirection.y != 0)
				_playerBrain.Weapon.TryFire(_aimDirection);
		}

		void FixedUpdate()
		{			
			UpdateMovement();
			UpdateAim();
		}

		public void SetDirectionMove(Vector2 direction) 
		{
			_moveDirection = direction;
		}

		public void SetDirectionAim(Vector2 direction) 
		{
			_aimDirection = direction;
		}

		void UpdateMovement() 
		{	
			if (!_playerBrain.PhotonView.IsMine) 
				return;
			
			var xVelocity = _moveDirection.x * _speed;
			var yVelocity = CalculateYVelocity();

			_playerBrain.Rigidbody.velocity = new Vector2(xVelocity, yVelocity);
			
			_playerBrain.Graphics.SetAnimatorValueLegs(
				_moveDirection.x != 0, 
				_isGrounded, 
				_playerBrain.Rigidbody.velocity.y
				);
		}

		void UpdateAim() 
		{
			var xVelocity = _aimDirection.x;
			var yVelocity = _aimDirection.y;

			if (xVelocity != 0 || yVelocity != 0) 
				_playerBrain.Graphics.SetAnimatorValueTorso(xVelocity, yVelocity);		
		}

		float CalculateYVelocity()
		{
			var yVelocity = _playerBrain.Rigidbody.velocity.y;
			var isJumpPressing = _moveDirection.y > 0;

			if (isJumpPressing) 
				yVelocity = CalculateJumpVelocity(yVelocity);

			return yVelocity;
		}

		float CalculateJumpVelocity(float yVelocity) 
		{
			var isFalling = _playerBrain.Rigidbody.velocity.y <= 0.001f;

			if (!isFalling) 
				return yVelocity;

			if (_isGrounded)
				yVelocity += _jumpSpeed;

			return yVelocity;
		}

		bool IsGrounded() 
		{
			return _groundCheck.IsTouchingLayer;
		}			

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
			if (stream.IsWriting)
			{
				stream.SendNext(_moveDirection);
				stream.SendNext(_isGrounded);
				stream.SendNext(_aimDirection);
			}
			else 
			{
				_moveDirection = (Vector2)stream.ReceiveNext();
				_isGrounded = (bool)stream.ReceiveNext();
				_aimDirection = (Vector2)stream.ReceiveNext();
			}
        }
    }
}

