using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PistolProjectile : MonoBehaviour
	{		
		[SerializeField] private int _damage = 1;
		private Rigidbody2D _rigidbody;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		public void SetVelocity(Vector2 direction, float force) 
		{
			_rigidbody.velocity = direction * force;
			Debug.Log("<Color=Red>Velocity:</Color> " + _rigidbody.velocity);
		}

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

