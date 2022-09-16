using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPun2D.pt2
{
	public class PlayerControls : MonoBehaviour, IPunObservable
	{
		[SerializeField] private float _speed = 8f;
		private PhotonView _photonView;
		private Rigidbody2D _rigidbody;
		private Vector2 _direction;
		[SerializeField] private SpriteRenderer _renderer;
		private bool _isRed;

		private void Start()
		{
			_photonView = GetComponent<PhotonView>();
			_rigidbody = GetComponent<Rigidbody2D>();	

			if (!_photonView.IsMine) 
				Destroy(_rigidbody);
		}

		//!----PLAYER---INPUT------------------------------------------------------------------------
		public void OnMovement(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine) 
				return;

			_direction = context.ReadValue<Vector2>();
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
		}

		private void FixedUpdate()
		{
			if (!_photonView.IsMine) 
				return;

			var xVelocity = _direction.x * _speed;
			var yVelocity = _direction.y * _speed;

			_rigidbody.velocity = new Vector2(xVelocity, yVelocity);
		}

		private void UpdateSpriteDirection() 
		{
			if (_direction.x > 0) 
				_renderer.transform.localScale = new Vector3(1, 1, 1);
			else if (_direction.x < 0) 
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
				stream.SendNext(_direction);
			}
			else 
			{
				_isRed = (bool)stream.ReceiveNext();
				_direction = (Vector2)stream.ReceiveNext();
			}
        }
    }
}

