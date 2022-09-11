using UnityEngine;

namespace ShooterPun2D
{
	public class PickupWeapon : Pickup
	{
		[SerializeField] private WeaponType _weaponType;

		protected override void OnTriggerEnter2D(Collider2D other)
		{
			PlayerWeapon weapon = other.gameObject.GetComponent<PlayerWeapon>();
			if (weapon) 
			{
				weapon.Weapons[(int)_weaponType].IsActive = true;
				weapon.Weapons[(int)_weaponType].AmmoCount += _amount;
				weapon.SetWeapon((int)_weaponType);
				base.OnTriggerEnter2D(other);
			}
		}
	}
}

