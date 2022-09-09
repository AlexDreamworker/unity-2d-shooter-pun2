using UnityEngine;

namespace ShooterPun2D
{
	public class PickupPistolAmmo : Pickup
	{
		protected override void OnTriggerEnter2D(Collider2D other)
		{
			PlayerWeapon weapon = other.gameObject.GetComponent<PlayerWeapon>();
			if (weapon) 
			{
				weapon.Weapons[0].AmmoCount += _amount;
				base.OnTriggerEnter2D(other);
			}
		}
	}
}

