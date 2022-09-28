using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerModel : MonoBehaviour/*PunCallbacks*/
	{
		[SerializeField] private GameObject[] _skins;
		private GameObject _currentSkin;

		private int _currentSkinIndex;

		private Player _player;
		
		//private ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();

		private void Start()
		{
			_currentSkin = _skins[0];
			_currentSkinIndex = 0;
			UpdateSkin();

			//_playerProperties["playerModel"] = 0;
		}

		//* private void Update()
		//* {
		//* 	Debug.Log("Prefs is: " + PlayerPrefs.GetInt("PlayerModelIndex"));
		//* }

		public void NextSkin() //* CALL
		{
			// if (_currentSkin == _skins[0])
			// 	_currentSkin = _skins[1];
			// else 
			// 	_currentSkin = _skins[0];
			_currentSkin = _skins[1];
			_currentSkinIndex = 1;

			UpdateSkin();

			// if ((int)_playerProperties["playerModel"] == _skins.Length - 1) 
			// {
			// 	_playerProperties["playerModel"] = 0;
			// }
			// else 
			// {
			// 	_playerProperties["playerModel"] = (int)_playerProperties["playerModel"] + 1;
			// }

			//_playerProperties["playerModel"] = 1;
			//PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
		}

		public void PreviousSkin() //* CALL
		{
			// if (_currentSkin == _skins[1])
			// 	_currentSkin = _skins[0];
			// else 
			// 	_currentSkin = _skins[1];
			_currentSkin = _skins[0];
			_currentSkinIndex = 0;
			
			UpdateSkin();


			//if ((int)_playerProperties["playerModel"] == 0) 
			//{
				//_playerProperties["playerModel"] = _skins.Length - 1;
			//}
			//else 
			//{
				//_playerProperties["playerModel"] = (int)_playerProperties["playerModel"] - 1;
			//}
			//_playerProperties["playerModel"] = 0;
			//PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
		}

		private void UpdateSkin() 
		{
			foreach (var skin in _skins)
			{
				skin.SetActive(false);
			}

			_currentSkin.SetActive(true);
			
			PlayerPrefs.SetInt("PlayerModelIndex", _currentSkinIndex);
		}

		// public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable hash) 
		// {
		// 	if (_player == targetPlayer) 
		// 	{
		// 		UpdatePlayerItem(targetPlayer);
		// 	}
		// }

		// private void UpdatePlayerItem(Player player) 
		// {
		// 	if (player.CustomProperties.ContainsKey("playerModel")) 
		// 	{
		// 		_currentSkin = _skins[(int)player.CustomProperties["playerModel"]];
		// 		_playerProperties["playerModel"] = (int)player.CustomProperties["playerModel"];
		// 	}
		// 	else 
		// 	{
		// 		_playerProperties["playerModel"] = 0;
		// 	}

		// 	UpdateSkin();
		// }
	}
}

