using System;
using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerWeapon : MonoBehaviour, IPunObservable
	{
		public event Action<int, Color> OnAmmoChanged;
		public event Action<int> OnWeaponChanged;
		public event Action<int> OnWeaponActivated;
		public event Action<int, bool> OnAmmoEmpted;
		public event Action OnWeaponRefreshed; //todo: rename!

		[SerializeField] private Weapon[] _weapons;
		private Weapon _currentWeapon;

		public Weapon[] Weapons => _weapons;

		private int _currentAmmoCount;
		
		[SerializeField] private Animator _bodyTorsoAnim;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private SpriteRenderer _shootPointColor;
		[SerializeField] private GameObject _aimPoint;
		//[SerializeField] private GameObject _pistolProjectile;
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
			SetAimAnimation();
			SetWeaponOnStart();
			//SetWeapon(0);

			if (!_photonView.IsMine) 
			{
				_aimPoint.SetActive(false);
			}
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

		public void SetAimAnimation() //* CALL
		{
			_bodyTorsoAnim.SetFloat("x", 1);
			_bodyTorsoAnim.SetFloat("y", 0);
		}

		public void SetWeaponOnStart() //* CALL
		{
			foreach (var weapon in _weapons) 
			{
				if (weapon.Id == 0) 
				{
					weapon.IsActive = true;
					weapon.AmmoCount = 777;
					SetWeapon(weapon.Id);
				}
				else 
				{
					weapon.IsActive = false;
					weapon.AmmoCount = 0;
				}
			}

			OnWeaponRefreshed?.Invoke();
		}

		private void TryFire() 
		{
			CheckAmmunition();

			_currentAmmoCount = _currentWeapon.AmmoCount;

			if (Time.time > _shootCooldown) 
			{
				_photonView.RPC("RpcShoot", RpcTarget.All, _direction, _bulletForce);
				_shootCooldown = Time.time + 1 / _fireRate;
			}
		}

		public void ShootPointColorActivity(bool isActive) //* CALL
		{
			_shootPointColor.enabled = isActive;
		}

		[PunRPC]
		private void RpcShoot(Vector2 dir, float force) 
		{
			GameObject bullet = Instantiate(_currentWeapon.ProjectilePrefab, _shootPoint.position, Quaternion.identity);
			bullet.GetComponent<PistolProjectile>().SetVelocity(dir, force);

			_currentAmmoCount -= 1;
			_currentWeapon.AmmoCount = _currentAmmoCount;
			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);
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

		//!-------W-E-A-P-O-N-S----L-O-G-I-C------------------------------------

		public void SetWeapon(int index) //* CAll
		{
			_photonView.RPC("RpcWeaponChange", RpcTarget.All, index);
		}

		[PunRPC]
		public void RpcWeaponChange(int index)
		{
			_currentWeapon = _weapons[index];
			_shootPointColor.color = _currentWeapon.Color;

			OnWeaponChanged?.Invoke(_currentWeapon.Id);
			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);
		}
		
		public void CheckAmmunition()
		{
			if (_currentWeapon.AmmoCount == 0)
			{
				OnAmmoEmpted?.Invoke(_currentWeapon.Id, true);
				NextWeapon();
			}
		}

		public void SetAmmunition(int index, int value) //*CALL
		{
			_weapons[index].AmmoCount += value;
			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);
			OnAmmoEmpted?.Invoke(index, false);
		}

		public void SetWeaponActivity(int index) //*CALL
		{
			_weapons[index].IsActive = true;
			OnWeaponActivated?.Invoke(index);
			SetWeapon(index);
		}

		public void NextWeapon() //* CALL
		{
			foreach (var weapon in _weapons.Where(w => w.IsActive)) 
			{
				if (weapon.Id > _currentWeapon.Id && weapon.AmmoCount != 0) 
				{
					SetWeapon(weapon.Id);
					return;
				}
			}	
			var firstActiveId = _weapons.Where(w => w.IsActive).First().Id;
			SetWeapon(firstActiveId);
		}

		public void PreviousWeapon() //* CALL 
		{	
			foreach (var weapon in _weapons.Where(w => w.IsActive).Reverse()) //! where is bug?
			{
				if (weapon.Id < _currentWeapon.Id && weapon.AmmoCount != 0)
				{
					SetWeapon(weapon.Id);
					return;
				}
			}
			var lastActiveId = _weapons.Where(w => w.IsActive).Last().Id;
			SetWeapon(lastActiveId);
		}
    }
}
