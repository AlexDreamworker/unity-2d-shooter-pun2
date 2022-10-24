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
		public event Action OnWeaponRefreshed;

		[SerializeField] private Transform _shootPoint;
		[SerializeField] private float _bulletForce = 1000f; 
		[SerializeField] private float _fireRate; 
		[SerializeField] private Weapon[] _weapons;

		private Weapon _currentWeapon; 
		private int _currentWeaponIndex; 
		private int _currentAmmoCount;
		private float _shootCooldown;
		private PlayerBrain _playerBrain;

		public Weapon[] Weapons => _weapons;
		
		void Awake()
		{
			_playerBrain = GetComponent<PlayerBrain>();
		}

		void Start() => RefreshWeapon();

		void Update()
		{
			_currentWeapon = _weapons[_currentWeaponIndex];
		}

		#region SHOOT
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
		void RpcShoot(Vector2 dir, float force, PhotonMessageInfo info) 
		{
			GameObject bullet = Instantiate(_currentWeapon.ProjectilePrefab, _shootPoint.position, Quaternion.identity);
			bullet.GetComponent<PistolProjectile>().SetPlayer(_playerBrain.PhotonView, info.Sender);
			bullet.GetComponent<PistolProjectile>().SetVelocity(dir, force);

			_currentAmmoCount -= 1;
			_currentWeapon.AmmoCount = _currentAmmoCount;
			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);
		}
		#endregion

		#region SET WEAPON
		public void SetWeapon(int index)
		{
			_currentWeaponIndex = index;
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
		#endregion

		#region AMMUNITION
		public void CheckAmmunition()
		{
			if (_currentWeapon.AmmoCount == 0)
			{
				OnAmmoEmpted?.Invoke(_currentWeapon.Id, true);
				NextWeapon();
			}
		}

		public void SetAmmunition(int index, int value)
		{
			_weapons[index].AmmoCount += value;
			OnAmmoChanged?.Invoke(_currentWeapon.AmmoCount, _currentWeapon.Color);
			OnAmmoEmpted?.Invoke(index, false);
		}
		#endregion
		
		#region CALLBACKS
		public void NextWeapon()
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

		public void PreviousWeapon()
		{	
			foreach (var weapon in _weapons.Where(w => w.IsActive).Reverse())
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

		public void SetWeaponActivity(int index)
		{
			_weapons[index].IsActive = true;
			OnWeaponActivated?.Invoke(index);
			SetWeapon(index);
		}

		public void RefreshWeapon()
		{
			if (_playerBrain.PhotonView.IsMine) 
			{
				_currentWeaponIndex = 0;

				foreach (var weapon in _weapons) 
				{
					if (weapon.Id == 0)
					{
						weapon.IsActive = true;
						weapon.AmmoCount = 777; 
					}
					else 
					{
						weapon.IsActive = false;
					}
				}	

				OnWeaponRefreshed?.Invoke();
			}

			SetWeapon(_currentWeaponIndex);
		}
		#endregion

		#region  SERIALIZATOR
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
				stream.SendNext(_currentWeaponIndex);
			else 
				_currentWeaponIndex = (int)stream.ReceiveNext();
        }
		#endregion
    }
}

