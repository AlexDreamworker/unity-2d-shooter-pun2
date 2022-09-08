using UnityEngine;

namespace ShooterPun2D
{
	public class WeaponPistol : Weapon
	{
		private int _pistolAmmo;

        public override void Shoot()
        {
			_pistolAmmo = _ammunition.PistolAmmo;

			if (_pistolAmmo <= 0)
				return;

            base.Shoot();
			_pistolAmmo -= 1;
			_ammunition.PistolAmmo = _pistolAmmo;
        }		
	}
}

