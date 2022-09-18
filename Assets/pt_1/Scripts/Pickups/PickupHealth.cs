using UnityEngine;

namespace ShooterPun2D.pt1
{
	public class PickupHealth : Pickup
	{
		protected override void OnTriggerEnter2D(Collider2D other)
		{
			PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
			if (health) 
			{
				health.Heal(_amount);
				base.OnTriggerEnter2D(other);
			}					
		}
	}
}

