using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace ShooterPun2D
{
	public class PlayerWeapon : MonoBehaviourPunCallbacks
	{
		public event Action<int, Color> OnAmmoChanged;
		public event Action<int> OnWeaponChanged;

		[SerializeField] private GameObject _weaponHolder;
		[SerializeField] private Weapon[] _weapons;
		[SerializeField] private Weapon _currentWeapon;

		private GameObject[] _weaponsGraphics;
		private GameObject _currentWeaponGraphics;

		private Vector2 _direction;
		private float _timeToShoot;
		private bool _isStartFire;

		private Dictionary<Weapon, bool> _weaponsMap = new Dictionary<Weapon, bool>();

		private PhotonView _photonView;

		private ExitGames.Client.Photon.Hashtable _weaponIndexProperties = new ExitGames.Client.Photon.Hashtable(); //?

		public GameObject WeaponHolder => _weaponHolder;
		public Weapon[] Weapons => _weapons;

		public bool IsStartFire
		{
			get => _isStartFire;
			set => _isStartFire = value;
		}

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			InitializeWeaponsGraphic();
			UpdateWeaponsMap();

			if (_photonView.IsMine)
			{
				_currentWeapon = _weapons[0];
				_weaponIndexProperties["weaponIndex"] = _currentWeapon.Id;
			}

			SetWeapon(_currentWeapon.Id);
		}

		private void Update()
		{
			UpdateAim();
			UpdateWeaponsMap(); //? Test Function were
			
			if (_isStartFire)
			{
				TryFire();
			}
		}

		public void SetDirection(Vector2 direction)
		{
			_direction = direction;
		}

		public void UpdateAim()	
		{
			_weaponHolder.transform.right = _direction;
		}

		private void InitializeWeaponsGraphic()
		{
			_weaponsGraphics = new GameObject[_weapons.Length];

			for (int i = 0; i < _weapons.Length; i++) 
			{
				_weaponsGraphics[i] = _weaponHolder.transform.GetChild(i).gameObject;
			}	
		}

		public void UpdateWeaponsMap() //* CALL
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
			CheckAmmunition();
			
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

			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);			
		}

		public void SetWeapon(int index) //* CALL
		{
			_currentWeapon = _weapons[index];

			foreach (var item in _weaponsGraphics)
			{
				item.SetActive(false);
			}

			_currentWeaponGraphics = _weaponsGraphics[index];
			_currentWeaponGraphics.SetActive(true);

			OnWeaponChanged?.Invoke(_currentWeapon.Id);
			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);

			if (_photonView.IsMine) 
			{
				//?Hashtable hash = new Hashtable();
				//?hash.Add("weaponIndex", index);
				//?PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

				_weaponIndexProperties["weaponIndex"] = index;
				PhotonNetwork.SetPlayerCustomProperties(_weaponIndexProperties);
			}
		}

		public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) 
		{
			if (!_photonView.IsMine && targetPlayer == _photonView.Owner)
			{
				if (changedProps.ContainsKey("weaponIndex"))
					SetWeapon((int)changedProps["weaponIndex"]);
			}
		}

		public void CheckAmmunition()
		{
			if (_currentWeapon.AmmoCount == 0)
			{
				NextWeapon();
			}
		}

		public void SetAmmunition(int index, int value) //*CALL
		{
			_weapons[index].AmmoCount += value;
			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);
		}

		public void SetWeaponActivity(int index) //*CALL
		{
			_weapons[index].IsActive = true;
			UpdateWeaponsMap();
			SetWeapon(index);
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

