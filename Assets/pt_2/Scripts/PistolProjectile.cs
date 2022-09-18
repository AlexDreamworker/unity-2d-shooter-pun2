using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PistolProjectile : MonoBehaviour
	{		
		[SerializeField] private int _damage = 1;
		private Rigidbody2D _rigidbody;

		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		// private void OnCollisionEnter2D(Collision2D other)
		// {
		// 	PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
		// 	if (health != null)
		// 	{
		// 		health.TakeDamage(_damage);
		// 		Destroy(this.gameObject);
		// 	}

		// 	else if (other.gameObject.CompareTag("Ground")) 
		// 	{
		// 		Destroy(this.gameObject);
		// 	}
		// }

		private void OnTriggerEnter2D(Collider2D other)
		{
			PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
			if (health != null)
			{
				health.TakeDamage(_damage);
				Destroy(this.gameObject);
			}

			else if (other.gameObject.CompareTag("Ground")) 
			{
				Destroy(this.gameObject);
			}
		}
	}
}

