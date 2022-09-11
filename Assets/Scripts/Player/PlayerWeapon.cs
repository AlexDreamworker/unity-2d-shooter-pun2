using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace ShooterPun2D
{
	public class PlayerWeapon : MonoBehaviour
	{
		[SerializeField] private GameObject _weaponHolder;
		[SerializeField] private int _currentWeaponIndex;
		[SerializeField] private Weapon[] _weapons;

		private Weapon _currentWeapon;
		private GameObject[] _weaponsGraphics;
		private GameObject _currentWeaponGraphics;
		private int _weaponsCount;
		private float _timeToShoot;

		public Weapon[] Weapons => _weapons;
		public GameObject WeaponHolder => _weaponHolder;

		private Dictionary<Weapon, bool> _weaponsMap = new Dictionary<Weapon, bool>();

		private void Start()
		{
			_weaponsCount = _weapons.Length;
			InitializeWeaponsGraphic();
			SetDefaultWeaponOnStart();			
			UpdateWeaponsActivity();
		}

		private void Update()
		{
			_currentWeapon = _weapons[_currentWeaponIndex];
			CheckAmmunition();
		}

		private void InitializeWeaponsGraphic()
		{
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

		private void UpdateWeaponsActivity()
		{
			_weaponsMap.Clear();

			foreach (var weapon in _weapons)
			{
				if (weapon.IsActive)
				{
					_weaponsMap.Add(weapon, weapon.IsActive);
				}
			}
		}

		public void TryFire() //* CALL
		{
			if (Time.time > _timeToShoot) 
			{
				_timeToShoot = Time.time + 1 / _currentWeapon.FireRate;
				Shoot();
			}
		}	

		private void Shoot() 
		{
			var currentAmmoCount = _currentWeapon.AmmoCount;

			if (_currentWeapon.AmmoCount <= 0)
				return;

			GameObject projectile = Instantiate(
				_currentWeapon.ProjectilePrefab, 
				_currentWeapon.ShootPoint.position, 
				_currentWeapon.ShootPoint.rotation
				);

			var projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
			projectileRigidbody.AddForce(projectile.transform.right * _currentWeapon.ProjectileSpeed);

			currentAmmoCount -= 1;
			_currentWeapon.AmmoCount = currentAmmoCount;			
		}

		public void SetWeapon(int index)
		{
			UpdateWeaponGraphics(_currentWeaponIndex, index);
			_currentWeaponIndex = index;
		}	

		//?-----------------------------------------------------------------------------------------------
		//TODO: refact
		public void CheckAmmunition()
		{
			if (_currentWeapon.AmmoCount == 0)
			{
				NextWeapon();
			}
		}

		//TODO: refact
		public void NextWeapon() //* CALL
		{
			if (_currentWeaponIndex < _weaponsCount - 1) 
			{
				TrySwitchWeapon(true);
			}

			// else if (_currentWeaponIndex == _weaponsCount - 1)
			// {
			// 	SetWeapon(0);
			// }
		}

		//TODO: refact
		public void PreviousWeapon() //* CALL
		{
			if (_currentWeaponIndex > 0) 
			{
				TrySwitchWeapon(false);
			}

			// else if (_currentWeaponIndex == 0)
			// {

			// }
		}

		//TODO: refact
		private void TrySwitchWeapon(bool isIncrease)
		{
			var oldWeaponIndex = _currentWeaponIndex;

			if (isIncrease)
			{
				foreach (var weapon in _weapons)
				{			
					if ((int)weapon.WeaponType > _currentWeaponIndex && weapon.IsActive && weapon.AmmoCount != 0)
					{
						SetWeapon((int)weapon.WeaponType);
						UpdateWeaponGraphics(oldWeaponIndex, _currentWeaponIndex);
						return;
					}
				}
			}
			else 
			{
				foreach (var weapon in _weapons.Reverse())
				{			
					if ((int)weapon.WeaponType < _currentWeaponIndex && weapon.IsActive && weapon.AmmoCount != 0)
					{
						SetWeapon((int)weapon.WeaponType);
						UpdateWeaponGraphics(oldWeaponIndex, _currentWeaponIndex);
						return;
					}
				}
			}			
		}

		// private void SetLastActiveWeapon()
		// {
		// 	var lastActive = _weaponsMap.Keys.Last();
			
		// }

		// private Weapon GetWeaponActivity(Weapon weapon)
		// {
		// 	var result = weapon.IsActive ? weapon : null;
		// 	return result;
		// }
		//?-----------------------------------------------------------------------------------------------

		private void UpdateWeaponGraphics(int oldIndex, int newIndex) //! (int index)
		{
			_weaponsGraphics[oldIndex].SetActive(false);
			_weaponsGraphics[newIndex].SetActive(true);
			_currentWeaponGraphics = _weaponsGraphics[newIndex];
			//! for () 
			//!		all weapons -> false
			//!	weapon (index) -> true
		}
	}
}

