using UnityEngine;

namespace ShooterPun2D
{
	public class Projectile : MonoBehaviour
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Ground"))
			{
				Destroy(gameObject);
			}
		}
	}
}

