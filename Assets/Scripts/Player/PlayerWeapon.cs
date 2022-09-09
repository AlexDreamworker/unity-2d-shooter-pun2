using UnityEngine;
using System.Linq;

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

		private void Start()
		{
			_weaponsCount = _weapons.Length;
			InitializeWeaponsGraphic();
			SetDefaultWeaponOnStart();
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

		//TODO: refact
		public void CheckAmmunition()
		{
			if (_currentWeapon.AmmoCount == 0)
			{
				NextWeapon();
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

		//TODO: refact
		public void NextWeapon() //* CALL
		{
			if (_currentWeaponIndex < _weaponsCount - 1) 
			{
				TrySwitchWeapon(true);
			}
		}

		//TODO: refact
		public void PreviousWeapon() //* CALL
		{
			if (_currentWeaponIndex > 0) 
			{
				TrySwitchWeapon(false);
			}
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
						_currentWeaponIndex = (int)weapon.WeaponType;
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
						_currentWeaponIndex = (int)weapon.WeaponType;
						UpdateWeaponGraphics(oldWeaponIndex, _currentWeaponIndex);
						return;
					}
				}
			}			
		}

		private void UpdateWeaponGraphics(int oldIndex, int newIndex)
		{
			_weaponsGraphics[oldIndex].SetActive(false);
			_weaponsGraphics[newIndex].SetActive(true);
			_currentWeaponGraphics = _weaponsGraphics[newIndex];
		}
	}
}

