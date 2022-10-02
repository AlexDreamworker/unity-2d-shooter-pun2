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

		private PhotonView _playerPhotonView;
		private Player _sender;

		public void SetPlayer(PhotonView playerPhotonId, Player sender)
		{
			_playerPhotonView = playerPhotonId;
			_sender = sender;
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
				if (_playerPhotonView.ViewID == photonView.ViewID) 
					return;
				else 
					health.TakeDamage(_damage, _sender); //!

				Destroy(this.gameObject);
			}
		}
	}
}

