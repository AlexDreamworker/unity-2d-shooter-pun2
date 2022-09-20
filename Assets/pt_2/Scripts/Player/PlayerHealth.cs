using System;
using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerHealth : MonoBehaviour
	{
		public event Action<int> OnHealthChanged;
		[SerializeField] private int _currentHealth = 100;

		//todo: refactoring?
		[SerializeField] private GameObject _bloodParticle = null;
		[SerializeField] private GameObject _chunkParticle = null;
		[SerializeField] private SpriteRenderer _legs;
		[SerializeField] private SpriteRenderer _torso;
		[SerializeField] private GameObject _canvasOverhead;
		
		private PlayerMovement _movement;
		private PlayerWeapon _weapon;
		private Rigidbody2D _rigidbody;
		private Collider2D _collider;
		private PlayerInputReader _playerInput;

		private int _maxHealth;
		private PhotonView _photonView;

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
			//todo: refactoring?
			_rigidbody = GetComponent<Rigidbody2D>();
			_collider = GetComponent<Collider2D>();
			_playerInput = GetComponent<PlayerInputReader>();
			_movement = GetComponent<PlayerMovement>();
			_weapon = GetComponent<PlayerWeapon>();
		}

		private void Update()
		{
			if (_currentHealth <= 0) 
			{
				_photonView.RPC("Death", RpcTarget.All);
				return;
			}
		}

		private void Start()
		{
			OnHealthChanged?.Invoke(_currentHealth);
		}

		public void TakeDamage(int value) 
		{
			_photonView.RPC("Damage", RpcTarget.All, value);
		}

        [PunRPC]
        private void Damage(int value) 
        {
        	_currentHealth -= value;
			OnHealthChanged?.Invoke(_currentHealth);
        }

		[PunRPC]
		private void Death() 
		{
			_bloodParticle.SetActive(true);
			_chunkParticle.SetActive(true);
			if (_photonView.IsMine) 
			{
				_rigidbody.bodyType = RigidbodyType2D.Kinematic;
			}
			_collider.enabled = false;
			_playerInput.enabled = false;
			_movement.enabled = false;
			_weapon.enabled = false;
			_canvasOverhead.SetActive(false);
			_legs.enabled = false;
			_torso.enabled = false;
			return;
		}
    }
}

