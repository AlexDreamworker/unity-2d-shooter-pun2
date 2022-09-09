using System;
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

		public void SetNextWeapon() //* CALL
		{
			if (_currentWeaponIndex < _weapons.Length - 1) 
			{
				UpdateWeaponGraphics(true);
			}
		}

		public void SetPreviousWeapon() //* CALL
		{
			if (_currentWeaponIndex > 0) 
			{
				UpdateWeaponGraphics(false);
			}
		}

		private void UpdateWeaponGraphics(bool isIncrease)
		{
			_weaponsGraphics[_currentWeaponIndex].SetActive(false);
			var index = isIncrease ? _currentWeaponIndex++ : _currentWeaponIndex--;
			_timeToShoot = Time.time;
			_weaponsGraphics[_currentWeaponIndex].SetActive(true);
			_currentWeaponGraphics = _weaponsGraphics[_currentWeaponIndex];
		}
	}
}

