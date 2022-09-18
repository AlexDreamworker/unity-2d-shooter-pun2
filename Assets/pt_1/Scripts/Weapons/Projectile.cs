using UnityEngine;

namespace ShooterPun2D.pt1
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

