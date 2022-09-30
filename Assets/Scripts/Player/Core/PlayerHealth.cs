using System;
using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerHealth : MonoBehaviour
	{
		public event Action<int> OnHealthChanged;
		public event Action OnPlayerDead;

		[SerializeField] private int _currentHealth = 100;
		private int _maxHealth = 100;
		private PlayerBrain _playerBrain;

		public int Health 
		{
			get => _currentHealth;
			set 
			{
				_currentHealth = Mathf.Clamp(value, 0, _maxHealth);
				if (_currentHealth <= 0) 
				{
					OnPlayerDead?.Invoke();
				}
			}
		}

		private void Awake()
		{
			_playerBrain = GetComponent<PlayerBrain>();
		}

		private void Start()
		{
			OnHealthChanged?.Invoke(Health);
		}

		public void TakeDamage(int value) //* CALL //todo: Change Name!
		{
			if (!_playerBrain.PhotonView.IsMine)
				return;

			_playerBrain.PhotonView.RPC(nameof(RpcDamage), RpcTarget.All, value);
		}

        [PunRPC]
        private void RpcDamage(int value) 
        {
        	Health += value;
			OnHealthChanged?.Invoke(Health);
        }
    }
}

