using System.Collections;
using UnityEngine;

namespace ShooterPun2D
{
	public class PickupHealth : MonoBehaviour
	{
		[SerializeField] private int _amount;
		[SerializeField] private float _timeToRespawn;

		private SpriteRenderer _sprite;
		private Collider2D _collider;

		private void Awake()
		{
			_sprite = GetComponent<SpriteRenderer>();
			_collider = GetComponent<Collider2D>();	
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
			if (health) 
			{
				health.Heal(_amount);
				StartCoroutine("Respawn");
			}
		}

		private IEnumerator Respawn() 
		{
			_sprite.enabled = false;
			_collider.enabled = false;
			yield return new WaitForSeconds(_timeToRespawn);
			_sprite.enabled = true;
			_collider.enabled = true;
		}
	}
}

