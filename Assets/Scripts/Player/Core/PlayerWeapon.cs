using System;
using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerWeapon : MonoBehaviour //!---214
	{
		public event Action<int, Color> OnAmmoChanged;
		public event Action<int> OnWeaponChanged;
		public event Action<int> OnWeaponActivated;
		public event Action<int, bool> OnAmmoEmpted;
		public event Action OnWeaponRefreshed; //todo: rename!

		[SerializeField] private Weapon[] _weapons;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private float _bulletForce = 1000f; //?
		[SerializeField] private float _fireRate; //?

		private Weapon _currentWeapon;
		private int _currentAmmoCount;
		private float _shootCooldown;
		private PlayerBrain _playerBrain;

		public Weapon[] Weapons => _weapons;

		private void Awake()
		{
			_playerBrain = GetComponent<PlayerBrain>();
		}

		private void Start()
		{
			SetWeaponOnStart();
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

		public void TryFire(Vector2 direction)
		{
			CheckAmmunition();

			_currentAmmoCount = _currentWeapon.AmmoCount;

			if (Time.time > _shootCooldown) 
			{
				_playerBrain.PhotonView.RPC(nameof(RpcShoot), RpcTarget.All, direction, _bulletForce);
				_shootCooldown = Time.time + 1 / _fireRate;
			}
		}

		[PunRPC]
		private void RpcShoot(Vector2 dir, float force) 
		{
			GameObject bullet = Instantiate(_currentWeapon.ProjectilePrefab, _shootPoint.position, Quaternion.identity);
			bullet.GetComponent<PistolProjectile>().SetPlayer(_playerBrain.PhotonView);
			bullet.GetComponent<PistolProjectile>().SetVelocity(dir, force);

			_currentAmmoCount -= 1;
			_currentWeapon.AmmoCount = _currentAmmoCount;
			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);
		}

		public void SetWeapon(int index) //* CAll
		{
			_playerBrain.PhotonView.RPC(nameof(RpcWeaponChange), RpcTarget.All, index);
		}

		[PunRPC]
		public void RpcWeaponChange(int index)
		{
			_currentWeapon = _weapons[index];
			_playerBrain.Graphics.SetShootPointColor(_currentWeapon.Color);

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
