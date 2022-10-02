using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PickupHealth : Pickup
	{
		protected override void OnTriggerEnter2D(Collider2D other)
		{
			PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
			if (health) 
			{
				health.TakeHealth(_amount); //todo: Name refactor!
				base.OnTriggerEnter2D(other);
			}					
		}
	}
}

