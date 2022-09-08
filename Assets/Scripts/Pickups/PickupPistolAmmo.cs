using UnityEngine;

namespace ShooterPun2D
{
	public class PickupPistolAmmo : Pickup
	{
		protected override void OnTriggerEnter2D(Collider2D other)
		{
			PlayerAmmunition ammunition = other.gameObject.GetComponent<PlayerAmmunition>();
			if (ammunition) 
			{
				ammunition.PistolAmmo += _amount;
				base.OnTriggerEnter2D(other);
			}
		}
	}
}

