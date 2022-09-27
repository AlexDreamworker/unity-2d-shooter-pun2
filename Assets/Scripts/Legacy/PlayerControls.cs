using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPun2D.pt2
{
	public class PlayerControls : MonoBehaviour, IPunObservable
	{
		[SerializeField] private Animator _bodyLegsAnim;
		[SerializeField] private Animator _bodyTorsoAnim;
		[SerializeField] private SpriteRenderer _rendererLegs;
		[SerializeField] private SpriteRenderer _rendererTorso;
		[SerializeField] private GameObject _inputCanvas;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private GameObject _pistolProjectile;
		[SerializeField] private float _bulletForce = 1000f;
		[SerializeField] private Camera _camera;
		[SerializeField] private float _speed = 8f;
		[SerializeField] private float _fireRate;
		private float _shootCooldown;

		[SerializeField] private ScriptableObject[] _weapons;

		[Space]
		[SerializeField] private int _health = 100;
		
		private PhotonView _photonView;
		private Rigidbody2D _rigidbody;
		
		private Vector2 _moveDirection;
		private Vector2 _aimDirection;

		private bool _isYellow;

		private void Start()
		{
			_photonView = GetComponent<PhotonView>();
			_rigidbody = GetComponent<Rigidbody2D>();

			_bodyTorsoAnim.SetFloat("x", 1);
			_bodyTorsoAnim.SetFloat("y", 0);	

			if (!_photonView.IsMine) 
			{
				Destroy(_rigidbody);
				Destroy(_camera);
				Destroy(_inputCanvas);
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
			{
				_isYellow = true;
				//?Shoot();
			}

			if (context.canceled)
				_isYellow = false;
		}
		//!----PLAYER---INPUT------------------------------------------------------------------------

		private void Update()
		{
			UpdateSpriteDirection();
			//?SetYellowColor();
			TryFire();
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
				_rendererLegs.transform.localScale = new Vector3(1, 1, 1);
			else if (_moveDirection.x < 0) 
				_rendererLegs.transform.localScale = new Vector3(-1, 1, 1);
		}

		private void SetYellowColor() 
		{
			if (_isYellow) 
			{
				_rendererLegs.color = Color.yellow;
				_rendererTorso.color = Color.yellow;
			}
			else
			{
				_rendererLegs.color = Color.white;
				_rendererTorso.color = Color.white;
			} 
		}

		private void TryFire() 
		{
			if (_aimDirection.x == 0 && _aimDirection.y == 0)
				return;

			if (Time.time > _shootCooldown && (_aimDirection.x != 0 || _aimDirection.y != 0)) 
			{
				_shootCooldown = Time.time + 1 / _fireRate;
				_photonView.RPC("Shoot", RpcTarget.All);
			}
		}

		[PunRPC]
		private void Shoot() 
		{
			Vector2 bulletSpawnPoint = new Vector2(_shootPoint.position.x, _shootPoint.position.y);
			GameObject bullet = Instantiate(_pistolProjectile, bulletSpawnPoint, Quaternion.identity);
			bullet.GetComponent<Rigidbody2D>().AddForce(_aimDirection * _bulletForce);
		}

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
			if (stream.IsWriting)
			{
				stream.SendNext(_isYellow);
				stream.SendNext(_moveDirection);
				stream.SendNext(_aimDirection);
			}
			else 
			{
				_isYellow = (bool)stream.ReceiveNext();
				_moveDirection = (Vector2)stream.ReceiveNext();
				_aimDirection = (Vector2)stream.ReceiveNext();
			}
        }

		public void TakeDamage(int value) 
		{
			_photonView.RPC("Damage", RpcTarget.All, value);
		}

		[PunRPC]
		private void Damage(int value) 
		{
			_health -= value;
		}
    }
}

