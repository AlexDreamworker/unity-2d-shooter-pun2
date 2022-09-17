using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPun2D.pt2
{
	public class PlayerControls : MonoBehaviour, IPunObservable
	{
		[SerializeField] private Animator _bodyLegsAnim;
		[SerializeField] private Animator _bodyTorsoAnim;
		[SerializeField] private SpriteRenderer _renderer;
		[SerializeField] private Camera _camera;
		[SerializeField] private float _speed = 8f;
		
		private PhotonView _photonView;
		private Rigidbody2D _rigidbody;
		
		private Vector2 _moveDirection;
		private Vector2 _aimDirection;

		private bool _isRed;

		private void Start()
		{
			_photonView = GetComponent<PhotonView>();
			_rigidbody = GetComponent<Rigidbody2D>();	

			if (!_photonView.IsMine) 
			{
				Destroy(_rigidbody);
				Destroy(_camera);
			}
		}

		//!----PLAYER---INPUT------------------------------------------------------------------------
		public void OnMovement(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine) 
				return;

			_moveDirection = context.ReadValue<Vector2>();
		}

		public void OnAim(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine)
				return;
			
			//if (context.performed)
			//{
				var dir = context.ReadValue<Vector2>();
				var roundDir = Vector2Int.RoundToInt(dir);

				_aimDirection = roundDir;
			//}
		}

		public void OnShoot(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine) 
				return;

			if (context.started)
				_isRed = true;

			if (context.canceled)
				_isRed = false;
		}
		//!----PLAYER---INPUT------------------------------------------------------------------------

		private void Update()
		{
			UpdateSpriteDirection();
			Shoot();

			Debug.Log(_aimDirection);
		}

		private void FixedUpdate()
		{			
			UpdateMovement();
			UpdateAim();	
		}

		private void UpdateMovement() 
		{
			var xVelocity = _moveDirection.x * _speed;
			var yVelocity = _moveDirection.y * _speed;

			if (_photonView.IsMine) 
			{
				_rigidbody.velocity = new Vector2(xVelocity, yVelocity);
			}

			_bodyLegsAnim.SetBool("is-running", _moveDirection.x != 0);
		}

		private void UpdateAim() 
		{
			var xVelocity = _aimDirection.x;
			var yVelocity = _aimDirection.y;

			if (xVelocity != 0 || yVelocity != 0) 
			{
				_bodyTorsoAnim.SetFloat("x", xVelocity);
				_bodyTorsoAnim.SetFloat("y", yVelocity);
			}			
		}

		private void UpdateSpriteDirection() 
		{
			if (_moveDirection.x > 0) 
				_renderer.transform.localScale = new Vector3(1, 1, 1);
			else if (_moveDirection.x < 0) 
				_renderer.transform.localScale = new Vector3(-1, 1, 1);
		}

		private void Shoot() 
		{
			if (_isRed) 
				_renderer.color = Color.red;
			else 
				_renderer.color = Color.white;
		}

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
			if (stream.IsWriting)
			{
				stream.SendNext(_isRed);
				stream.SendNext(_moveDirection);
				stream.SendNext(_aimDirection);
			}
			else 
			{
				_isRed = (bool)stream.ReceiveNext();
				_moveDirection = (Vector2)stream.ReceiveNext();
				_aimDirection = (Vector2)stream.ReceiveNext();
			}
        }
    }
}

