using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PistolProjectile : MonoBehaviour
	{		
		[SerializeField] private int _damage = 1;
		[SerializeField] private ParticleSystem _sparksParticle;
		private Rigidbody2D _rigidbody;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		public void SetVelocity(Vector2 direction, float force) 
		{
			_rigidbody.velocity = direction * force;
			//Debug.Log("<Color=Red>Velocity:</Color> " + _rigidbody.velocity);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			other.gameObject.TryGetComponent(out PlayerHealth health);
			//var spawnPoint = other.gameObject.GetComponent<Collider2D>().ClosestPointOnBounds(transform.position);

			if (other.gameObject.CompareTag("Ground")) 
			{
				Instantiate(_sparksParticle, transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}

			if (health != null)
			{
				health.TakeDamage(_damage);
				Destroy(this.gameObject);
			}
		}
	}
}

