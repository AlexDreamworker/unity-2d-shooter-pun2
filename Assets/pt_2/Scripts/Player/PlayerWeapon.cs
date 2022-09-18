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
			TryFire();
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
			if (_direction.x == 0 && _direction.y == 0)
				return;

			if (Time.time > _shootCooldown && (_direction.x != 0 || _direction.y != 0)) 
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
			//bullet.GetComponent<Rigidbody2D>().AddForce(_direction * _bulletForce);
			bullet.GetComponent<Rigidbody2D>().velocity = _direction * _bulletForce;
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

