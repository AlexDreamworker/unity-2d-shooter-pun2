using System.Collections;
using System;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace ShooterPun2D.pt2
{
	public class PlayerHealth : MonoBehaviour
	{
		public event Action<int> OnHealthChanged;
		public event Action OnPlayerDead;

		[SerializeField] private int _currentHealth = 100;
		private int _maxHealth = 100;
		private PhotonView _photonView;

		//public int _kills;

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
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			OnHealthChanged?.Invoke(Health);
		}

		public void TakeDamage(int value) //* CALL
		{
			if (_photonView.IsMine)
			{
				_photonView.RPC(nameof(RpcDamage), RpcTarget.All, value);
			}
		}

        [PunRPC]
        private void RpcDamage(int value, PhotonMessageInfo info) 
        {
        	Health += value;
			OnHealthChanged?.Invoke(Health);

			// if (_currentHealth <= 0) 
			// {
			// 	GetKill();
			// }
        }

		// private void GetKill() 
		// {
		// 	_photonView.RPC(nameof(RpcGetKill), RpcTarget.All);
		// }

		// [PunRPC]
		// private void RpcGetKill() 
		// {
		// 	_kills++;

		// 	Hashtable hash = new Hashtable();
		// 	hash.Add("kills", _kills);
		// 	PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		// }
    }
}

