using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerMovement : MonoBehaviour, IPunObservable
	{
		[SerializeField] private Animator _bodyLegsAnim;
		[SerializeField] private SpriteRenderer _rendererLegs;
		[SerializeField] private LayerCheckComponent _groundCheck;
		[SerializeField] private float _speed;
		[SerializeField] private float _jumpSpeed = 500f;
		private Vector2 _direction;
		private Rigidbody2D _rigidbody;
		private PhotonView _photonView;

		private bool _isGrounded;
		private bool _isJumping;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			if (!_photonView.IsMine) 
			{
				Destroy(_rigidbody);
			}
		}

		private void Update()
		{
			_isGrounded = IsGrounded();
		}

		private void FixedUpdate()
		{			
			UpdateMovement();
			UpdateSpriteDirection();
		}

		private void UpdateMovement() 
		{	
			if (!_photonView.IsMine) 
				return;

			var xVelocity = _direction.x * _speed;
			var yVelocity = CalculateYVelocity();

			_rigidbody.velocity = new Vector2(xVelocity, yVelocity);

			_bodyLegsAnim.SetBool("is-running", _direction.x != 0);
			_bodyLegsAnim.SetBool("is-ground", _isGrounded);
			_bodyLegsAnim.SetFloat("vertical-velocity", _rigidbody.velocity.y); 
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

		private bool IsGrounded() 
		{
			return _groundCheck.IsTouchingLayer;
		}			

		private void UpdateSpriteDirection() 
		{
			if (_direction.x > 0) 
				_rendererLegs.transform.localScale = new Vector3(1, 1, 1);
			else if (_direction.x < 0) 
				_rendererLegs.transform.localScale = new Vector3(-1, 1, 1);
		}

		public void SetDirection(Vector2 direction) 
		{
			_direction = direction;
		}

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
			if (stream.IsWriting)
			{
				stream.SendNext(_direction);
				stream.SendNext(_isGrounded);
			}
			else 
			{
				_direction = (Vector2)stream.ReceiveNext();
				_isGrounded = (bool)stream.ReceiveNext();
			}
        }
    }
}

