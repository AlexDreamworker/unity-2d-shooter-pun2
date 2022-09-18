using UnityEngine;

namespace ShooterPun2D.pt1
{
	public class PickupWeapon : Pickup
	{
		[SerializeField] private WeaponType _weaponType;

		protected override void OnTriggerEnter2D(Collider2D other)
		{
			PlayerWeapon weapon = other.gameObject.GetComponent<PlayerWeapon>();
			if (weapon) 
			{
				weapon.SetAmmunition((int)_weaponType, _amount);
				weapon.SetWeaponActivity((int)_weaponType);
				base.OnTriggerEnter2D(other);
			}
		}
	}
}

