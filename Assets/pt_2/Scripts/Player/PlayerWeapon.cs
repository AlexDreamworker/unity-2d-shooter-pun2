using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerWeapon : MonoBehaviour, IPunObservable
	{
		[SerializeField] private Animator _bodyTorsoAnim;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private GameObject _pistolProjectile;
		[SerializeField] private float _bulletForce = 1000f;
		[SerializeField] private float _fireRate;
		private float _shootCooldown;
		private Vector2 _direction;
		private PhotonView _photonView;

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			_bodyTorsoAnim.SetFloat("x", 1);
			_bodyTorsoAnim.SetFloat("y", 0);
		}

		private void Update()
		{
			if (!_photonView.IsMine)
				return;

			if (_direction.x != 0 || _direction.y != 0)
			{
				TryFire();
			}		
		}

		private void FixedUpdate()
		{			
			UpdateAim();	
		}

		private void UpdateAim() 
		{
			var xVelocity = _direction.x;
			var yVelocity = _direction.y;

			if (xVelocity != 0 || yVelocity != 0) 
			{
				_bodyTorsoAnim.SetFloat("x", xVelocity);
				_bodyTorsoAnim.SetFloat("y", yVelocity);
			}			
		}

		private void TryFire() 
		{
			if (Time.time > _shootCooldown) 
			{
				_photonView.RPC("RpcShoot", RpcTarget.All, _direction, _bulletForce);
				_shootCooldown = Time.time + 1 / _fireRate;
			}
		}

		[PunRPC]
		private void RpcShoot(Vector2 dir, float force) 
		{
			//Vector2 bulletSpawnPoint = new Vector2(_shootPoint.position.x, _shootPoint.position.y);
			GameObject bullet = Instantiate(_pistolProjectile, _shootPoint.position, Quaternion.identity);
			//bullet.GetComponent<Rigidbody2D>().AddForce(_direction * _bulletForce);

			//bullet.GetComponent<Rigidbody2D>().velocity = _direction * _bulletForce;
			bullet.GetComponent<PistolProjectile>().SetVelocity(dir, force);
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
			}
			else 
			{
				_direction = (Vector2)stream.ReceiveNext();
			}
        }
    }
}

