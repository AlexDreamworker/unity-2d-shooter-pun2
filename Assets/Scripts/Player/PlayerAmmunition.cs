using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerAmmunition : MonoBehaviour
	{
		[SerializeField] private int _pistolAmmo;
		[SerializeField] private int _shotgunAmmo;
		[SerializeField] private int _automatAmmo;
		[SerializeField] private int _rocketAmmo;
		[SerializeField] private int _bfgAmmo;

		private int _maxAmmoCount = 999;

		public int PistolAmmo
		{
			get => _pistolAmmo;
			set
			{
				_pistolAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}

		public int ShotgunAmmo
		{
			get => _shotgunAmmo;
			set
			{
				_shotgunAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}
		
		public int AutomatAmmo
		{
			get => _automatAmmo;
			set
			{
				_automatAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}

		public int RocketAmmo
		{
			get => _rocketAmmo;
			set
			{
				_rocketAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}

		public int BfgAmmo
		{
			get => _bfgAmmo;
			set
			{
				_bfgAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}			
	}

	// public enum Weapons
	// {
	// 	pistol,
	// 	shotgun,
	// 	automat,
	// 	rocket,
	// 	bfg
	// }
}

