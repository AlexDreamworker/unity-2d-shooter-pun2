using UnityEngine;

namespace ShooterPun2D
{
	public class PickupAmmo : Pickup
	{
		[SerializeField] private WeaponType _weaponType;

		protected override void OnTriggerEnter2D(Collider2D other)
		{
			PlayerWeapon weapon = other.gameObject.GetComponent<PlayerWeapon>();
			if (weapon) 
			{
				weapon.Weapons[(int)_weaponType].AmmoCount += _amount;
				base.OnTriggerEnter2D(other);
			}
		}
	}
}

