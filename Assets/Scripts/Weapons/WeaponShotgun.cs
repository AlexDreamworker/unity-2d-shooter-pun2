using UnityEngine;

namespace ShooterPun2D
{
	public class WeaponShotgun : Weapon
	{
		private int _shotgunAmmo;
		
		public override void Shoot()
        {
			_shotgunAmmo = _ammunition.ShotgunAmmo;

			if (_shotgunAmmo <= 0)
				return;

            base.Shoot();
			_shotgunAmmo -= 1;
			_ammunition.ShotgunAmmo = _shotgunAmmo;
        }
	}
}

