using System.Collections;
using UnityEngine;

namespace ShooterPun2D
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Collider2D))]
	public class Pickup : MonoBehaviour
	{
		[SerializeField] protected int _amount;
		[SerializeField] private float _timeToRespawn;

		private SpriteRenderer _sprite;
		private Collider2D _collider;

		private readonly string RespawnRoutine = "Respawn";

		private void Awake()
		{
			_sprite = GetComponent<SpriteRenderer>();
			_collider = GetComponent<Collider2D>();	
		}

		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			StartCoroutine(RespawnRoutine);
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

