using UnityEngine;

namespace ShooterPun2D
{
	public class WeaponRocket : Weapon
	{
		private int _pocketAmmo;

		public override void Shoot()
        {
			_pocketAmmo = _ammunition.PistolAmmo;

			if (_pocketAmmo <= 0)
				return;

            base.Shoot();
			_pocketAmmo -= 1;
			_ammunition.PistolAmmo = _pocketAmmo;
        }
	}
}

