using UnityEngine;

namespace ShooterPun2D
{
	public class WeaponBfg : Weapon
	{
		private int _bfgAmmo;
		
		public override void Shoot()
        {
			_bfgAmmo = _ammunition.PistolAmmo;

			if (_bfgAmmo <= 0)
				return;

            base.Shoot();
			_bfgAmmo -= 1;
			_ammunition.PistolAmmo = _bfgAmmo;
        }
	}
}

