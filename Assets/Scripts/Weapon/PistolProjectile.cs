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

		private PhotonView _player;

		public void SetPlayer(PhotonView player)
		{
			_player = player;
		}
		
		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		// private void Start()
		// {
		// 	if (_player != null)
		// 		Debug.Log("Sender in Start: " + _player.ViewID);
		// 	else 
		// 		Debug.Log("Player is NULL on Start!");
		// }

		public void SetVelocity(Vector2 direction, float force) 
		{
			_rigidbody.velocity = direction * force;
			//Debug.Log("<Color=Red>Velocity:</Color> " + _rigidbody.velocity);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			other.gameObject.TryGetComponent(out PlayerHealth health);
			other.gameObject.TryGetComponent(out PhotonView photonView);
			//var spawnPoint = other.gameObject.GetComponent<Collider2D>().ClosestPointOnBounds(transform.position);

			if (other.gameObject.CompareTag("Ground")) 
			{
				Instantiate(_sparksParticle, transform.position, Quaternion.identity);
				Destroy(this.gameObject);
			}

			if (health != null)
			{
				// if (_player != null)
				// 	Debug.Log("Sender in Trigger: " + _player.ViewID);
				// else 
				// 	Debug.Log("Player is NULL on Trigger!");

				if (_player.ViewID == photonView.ViewID) 
				{
					//Debug.Log("LP self heart");
					return;
				}
				else 
				{
					//Debug.Log("LP attacked Other Player!");
					health.TakeDamage(_damage);
				}

				//health.TakeDamage(_damage);
				Destroy(this.gameObject);
			}
		}
	}
}

