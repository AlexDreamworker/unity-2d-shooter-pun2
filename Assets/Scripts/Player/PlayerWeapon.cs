using System;
using System.Linq;
using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerWeapon : MonoBehaviour
	{
		[SerializeField] private GameObject _weaponHolder;
		[SerializeField] private int _currentWeaponIndex;
		[SerializeField] private Weapon[] _weapons;

		private int _currentAmmoCount;
		private float _timeToShoot;

		private int _weaponsCount;
		private GameObject[] _weaponsGraphics;
		private GameObject _currentWeaponGraphics;

		public Weapon[] Weapons => _weapons;
		public GameObject WeaponHolder => _weaponHolder;

		private void Start()
		{
			InitializeWeapons();
			SetDefaultWeaponOnStart();
		}

		private void InitializeWeapons()
		{
			_weaponsCount = _weapons.Length;
			_weaponsGraphics = new GameObject[_weaponsCount];

			for (int i = 0; i < _weaponsCount; i++) 
			{
				_weaponsGraphics[i] = _weaponHolder.transform.GetChild(i).gameObject;
				_weaponsGraphics[i].SetActive(false);
			}			
		}

		private void SetDefaultWeaponOnStart()
		{
			_weaponsGraphics[0].SetActive(true);
			_currentWeaponGraphics = _weaponsGraphics[0];
			_currentWeaponIndex = 0;			
		}

		public void TryFire() //* CALL
		{
			var weapon = _weapons[_currentWeaponIndex];

			if (Time.time > _timeToShoot) 
			{
				_timeToShoot = Time.time + 1 / weapon.FireRate;
				Shoot();
			}
		}	

		private void Shoot() 
		{
			_currentAmmoCount = _weapons[_currentWeaponIndex].AmmoCount;

			if (_weapons[_currentWeaponIndex].AmmoCount <= 0)
				return;

			GameObject projectile = Instantiate(
				_weapons[_currentWeaponIndex].ProjectilePrefab, 
				_weapons[_currentWeaponIndex].ShootPoint.position, 
				_weapons[_currentWeaponIndex].ShootPoint.rotation
				);

			var projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
			projectileRigidbody.AddForce(projectile.transform.right * _weapons[_currentWeaponIndex].ProjectileSpeed);

			_currentAmmoCount -= 1;
			_weapons[_currentWeaponIndex].AmmoCount = _currentAmmoCount;			
		}

		public void NextWeapon() //* CALL
		{
			if (_currentWeaponIndex < _weapons.Length - 1) 
			{
				TrySwitchWeapon(true);
			}
		}

		public void PreviousWeapon() //* CALL
		{
			if (_currentWeaponIndex > 0) 
			{
				TrySwitchWeapon(false);
			}
		}

		private void TrySwitchWeapon(bool isIncrease)
		{
			if (isIncrease)
			{
				foreach (var weapon in _weapons)
				{			
					if ((int)weapon.WeaponType > _currentWeaponIndex && weapon.IsActive)
					{
						_currentWeaponIndex = (int)weapon.WeaponType;
						return;
					}
				}
			}
			else 
			{
				foreach (var weapon in _weapons.Reverse())
				{			
					if ((int)weapon.WeaponType < _currentWeaponIndex && weapon.IsActive)
					{
						_currentWeaponIndex = (int)weapon.WeaponType;
						return;
					}
				}
			}
			
		}
	}
}

