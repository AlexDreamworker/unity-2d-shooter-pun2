using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PickupAmmo : Pickup
	{
		[SerializeField] private WeaponType _weaponType;

		protected override void OnTriggerEnter2D(Collider2D other)
		{
			PlayerWeapon weapon = other.gameObject.GetComponent<PlayerWeapon>();
			
			if (weapon) 
			{
				weapon.SetAmmunition((int)_weaponType, _amount);
				base.OnTriggerEnter2D(other);
			}
		}
	}
}

