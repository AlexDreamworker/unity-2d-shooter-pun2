using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace ShooterPun2D.pt2
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Collider2D))]
	public class Pickup : MonoBehaviour
	{
		[SerializeField] protected int _amount;
		[SerializeField] private float _timeToRespawn;

		private SpriteRenderer _sprite;
		private Collider2D _collider;
		private Vector3 _originalScale;

		private readonly string RespawnRoutine = "Respawn";

		void Awake()
		{
			_sprite = GetComponent<SpriteRenderer>();
			_collider = GetComponent<Collider2D>();	
			_originalScale = transform.localScale;
		}

		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			StartCoroutine(RespawnRoutine);
		}

		void ScaleAnimation()
		{
			transform.localScale = Vector3.zero;
			transform.DOScale(_originalScale, 1f);
		}

		IEnumerator Respawn() 
		{
			_sprite.enabled = false;
			_collider.enabled = false;
			yield return new WaitForSeconds(_timeToRespawn);
			_sprite.enabled = true;
			_collider.enabled = true;
			ScaleAnimation();
		}
	}
}

