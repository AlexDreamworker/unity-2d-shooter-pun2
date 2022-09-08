using UnityEngine;

namespace ShooterPun2D
{
    public class WeaponAutomat : Weapon
    {
        private int _automatAmmo;
        
        public override void Shoot()
        {
			_automatAmmo = _ammunition.PistolAmmo;

			if (_automatAmmo <= 0)
				return;

            base.Shoot();
			_automatAmmo -= 1;
			_ammunition.PistolAmmo = _automatAmmo;
        }
    }
}

