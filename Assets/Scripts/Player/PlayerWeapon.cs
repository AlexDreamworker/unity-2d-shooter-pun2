using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace ShooterPun2D
{
	public class PlayerWeapon : MonoBehaviour
	{
		[SerializeField] private GameObject _weaponHolder;
		[SerializeField] private Weapon[] _weapons;

		public Weapon _currentWeapon;
		private GameObject[] _weaponsGraphics;
		private GameObject _currentWeaponGraphics;
		private int _weaponsCount;
		private float _timeToShoot;

		private Vector2 _direction;
		private bool _isStartFire;
		private Dictionary<Weapon, bool> _weaponsMap = new Dictionary<Weapon, bool>();

		public Weapon[] Weapons => _weapons;
		public GameObject WeaponHolder => _weaponHolder;

		public bool IsStartFire
		{
			get => _isStartFire;
			set => _isStartFire = value;
		}

		private void Start()
		{
			_weaponsCount = _weapons.Length;
			InitializeWeaponsGraphic();
			SetWeapon(0);	
		}

		private void Update()
		{
			_weaponHolder.transform.right = _direction;

			UpdateWeaponsActivity();
			CheckAmmunition();
			
			if (_isStartFire)
			{
				TryFire();
			}
		}

		public void SetDirection(Vector2 direction)
		{
			_direction = direction;
		}

		// public void Fire(bool fireState)
		// {
		// 	_isStartFire = fireState;
		// }			

		private void InitializeWeaponsGraphic()
		{
			_weaponsGraphics = new GameObject[_weaponsCount];

			for (int i = 0; i < _weaponsCount; i++) 
			{
				_weaponsGraphics[i] = _weaponHolder.transform.GetChild(i).gameObject;
			}			
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

		public void TryFire()
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

		public void SetWeapon(int index) //* CALL
		{
			_currentWeapon = _weapons[index];

			for (int i = 0; i < _weaponsCount; i++) 
			{
				_weaponsGraphics[i].SetActive(false);
			}

			_currentWeaponGraphics = _weaponsGraphics[index];
			_currentWeaponGraphics.SetActive(true);
		}

		public void CheckAmmunition()
		{
			if (_currentWeapon.AmmoCount == 0)
			{
				NextWeapon();
			}
		}

		public void NextWeapon() //* CALL
		{
			foreach (var weapon in _weaponsMap) 
			{
				if (weapon.Key.Id > _currentWeapon.Id && weapon.Key.AmmoCount != 0) 
				{
					SetWeapon(weapon.Key.Id);
					return;
				}
			}
			SetWeapon(_weaponsMap.Keys.First().Id);
		}

		public void PreviousWeapon() //* CALL 
		{	
			foreach (var weapon in _weaponsMap.Reverse()) //! where is bug?
			{
				if (weapon.Key.Id < _currentWeapon.Id && weapon.Key.AmmoCount != 0)
				{
					SetWeapon(weapon.Key.Id);
					return;
				}
			}
			SetWeapon(_weaponsMap.Keys.Last().Id);
		}
	}
}

