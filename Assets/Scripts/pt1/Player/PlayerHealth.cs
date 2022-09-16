using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerHealth : MonoBehaviour
	{
		public event Action<int> OnHealthChanged;

		[SerializeField] private int _currentHealth = 100;
		private int _maxHealth = 100;
		//private PhotonView _photonView;

		// private void Awake()
		// {
		// 	_photonView = GetComponent<PhotonView>();
		// }

		private void Start()
		{
			OnHealthChanged?.Invoke(_currentHealth);
		}

		// public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) 
		// {
		// 	if (!_photonView.IsMine && targetPlayer == _photonView.Owner)
		// 	{
		// 		if (changedProps.ContainsKey("healthValue"))
		// 			SetHealth((int)changedProps["healthValue"]);
		// 	}
		// }		

		// public void SetHealth(int value)
		// {
		// 	_currentHealth = value;
		// }

		public void Heal(int amount) 
		{
			_currentHealth += amount;
			_currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
			
			OnHealthChanged?.Invoke(_currentHealth);

			// if (_photonView.IsMine) 
			// {
			// 	_healthProperties["healthValue"] = _currentHealth;
			// 	PhotonNetwork.SetPlayerCustomProperties(_healthProperties);
			// }			
		}
	}
}

