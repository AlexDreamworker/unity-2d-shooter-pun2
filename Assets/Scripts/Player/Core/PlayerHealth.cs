using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerHealth : MonoBehaviour
	{
		public event Action<int> OnHealthChanged;
		public event Action OnPlayerDead;

		[Header("Preferences")]
		[SerializeField] private int _currentHealth = 100;
		[Space]

		[Header("Prefabs")]
		[SerializeField] private GameObject _deathChunkParticle = null;
		[SerializeField] private GameObject _deathBloodParticle = null;
		private int _maxHealth = 100;
		private PlayerBrain _playerBrain;

		public int Health //???
		{
			get => _currentHealth;
			set 
			{
				_currentHealth = Mathf.Clamp(value, 0, _maxHealth);
				if (_currentHealth <= 0) 
				{
					OnPlayerDead?.Invoke();
					ChangePlayerState(false);
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

		//!-----------------------------------------------------------------------
		public void TakeDamage(int value, Player shooter)
		{
			if (!_playerBrain.PhotonView.IsMine)
				return;

			_playerBrain.PhotonView.RPC(nameof(RpcDamage), RpcTarget.All, value, shooter);
		}

        [PunRPC]
        private void RpcDamage(int value, Player shooter)
        {
        	Health += value;
			OnHealthChanged?.Invoke(Health);

				if (_currentHealth <= 0) 
				{
					_playerBrain.Data.SetFrags(shooter);
				}
        }

		public void TakeHealth(int value) 
		{
			if (!_playerBrain.PhotonView.IsMine)
				return;

			_playerBrain.PhotonView.RPC(nameof(RpcTakeHealth), RpcTarget.All, value);
		}

		[PunRPC]
        private void RpcTakeHealth(int value) 
        {
        	Health += value;
			OnHealthChanged?.Invoke(Health);
        }
		//!-----------------------------------------------------------------------

		public void ChangePlayerState(bool isAlive) 
		{
			_playerBrain.PhotonView.RPC(nameof(RpcChangePlayerState), RpcTarget.All, isAlive);
		}

		[PunRPC]
		private void RpcChangePlayerState(bool isAlive) 
		{
			if (!isAlive) 
			{
				if (_playerBrain.PhotonView.IsMine) 
				{
					_playerBrain.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
					_playerBrain.Rigidbody.velocity = Vector2.zero;		
				}

				Instantiate(_deathChunkParticle, gameObject.transform.position, Quaternion.identity);
				Instantiate(_deathBloodParticle, gameObject.transform.position, Quaternion.identity);
				_playerBrain.ComponentsActivity(false);
			}		
			else
			{
				if (_playerBrain.PhotonView.IsMine) 
				{	
					_playerBrain.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
				}

				gameObject.transform.position = _playerBrain.Global.GetSpawnPoint();
				_playerBrain.ComponentsActivity(true);
				_playerBrain.Weapon.RefreshWeapon();
				TakeHealth(100);
			}
		}
    }
}

