using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PistolProjectile : MonoBehaviour
	{		
		[SerializeField] private int _damage = 1;
		[SerializeField] private ParticleSystem _sparksParticle;
		private Rigidbody2D _rigidbody;

		private PhotonView _shooterPhotonView;
		private Player _shooter;

		public void SetPlayer(PhotonView photonView, Player sender)
		{
			_shooterPhotonView = photonView;
			_shooter = sender;
		}
		
		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		public void SetVelocity(Vector2 direction, float force) 
		{
			_rigidbody.velocity = direction * force;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			other.gameObject.TryGetComponent(out PlayerHealth health);
			other.gameObject.TryGetComponent(out PhotonView photonView);

			if (other.gameObject.CompareTag("Ground")) 
			{
				Instantiate(_sparksParticle, transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}

			if (health != null)
			{
				if (_shooterPhotonView.ViewID == photonView.ViewID) 
					return;
				else 
					health.TakeDamage(_damage, _shooter);

				Destroy(this.gameObject);
			}
		}
	}
}

