using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;
using ExitGames.Client.Photon;

namespace ShooterPun2D.pt2
{
	public class NetworkManager : MonoBehaviourPunCallbacks
	{
		[SerializeField] private GameObject[] _playerPrefabs;
		[SerializeField] private Transform[] _spawnPoints;

		private GameObject _prefabToSpawn;

		private void Start()
		{
			if (PhotonNetwork.LocalPlayer.CustomProperties["playerModel"] != null)
			{
				_prefabToSpawn = _playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerModel"]];
				Debug.Log((int)PhotonNetwork.LocalPlayer.CustomProperties["playerModel"]);
			}
			else 
			{
				_prefabToSpawn = _playerPrefabs[0];
				Debug.Log("Properties is NULL");
			}

			var randomSpawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position;
			PhotonNetwork.Instantiate(_prefabToSpawn.name, randomSpawnPoint, Quaternion.identity);

			//Регистрация кастомного типа данных с сериализацией / десериализацией
			//?PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
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
		// }

		public void Leave() 
		{
			// Вызов для выхода из комнаты
			PhotonNetwork.LeaveRoom();
		}

		public override void OnLeftRoom() 
		{
			// Когда мы покидаем комнату
			SceneManager.LoadScene(0);
		}

		public override void OnPlayerEnteredRoom(Player newPlayer) 
		{ 
			Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
		}

		public override void OnPlayerLeftRoom(Player otherPlayer) 
		{
			Debug.LogFormat("Player {0} entered room", otherPlayer.NickName);
		}	

		//?----CUSTOM---TYPE---SERIALIZE-/-DESERIALIZE------------------------------------------------------
		// public static object DeserializeVector2Int(byte[] data) 
		// {
		// 	Vector2Int result = new Vector2Int();

		// 	result.x = BitConverter.ToInt32(data, 0);
		// 	result.y = BitConverter.ToInt32(data, 4);
			
		// 	return result;
		// }	

		// public static byte[] SerializeVector2Int(object obj) 
		// {
		// 	Vector2Int vector = (Vector2Int)obj;
		// 	byte[] result = new byte[8];

		// 	BitConverter.GetBytes(vector.x).CopyTo(result, 0);
		// 	BitConverter.GetBytes(vector.y).CopyTo(result, 4);

		// 	return result;
		// }
		//?----CUSTOM---TYPE---SERIALIZE-/-DESERIALIZE------------------------------------------------------
	}
}

