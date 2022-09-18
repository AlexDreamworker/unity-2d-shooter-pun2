using System;
using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerHealth : MonoBehaviour
	{
		public event Action<int> OnHealthChanged;
		[SerializeField] private int _currentHealth = 100;
		private int _maxHealth;
		private PhotonView _photonView;

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
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
    }
}

