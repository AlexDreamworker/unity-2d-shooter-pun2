using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;
using ExitGames.Client.Photon;
using Cinemachine;
using System.Collections.Generic;

namespace ShooterPun2D.pt2
{
	public class NetworkManager : MonoBehaviourPunCallbacks
	{
		[Header("PLAYER SPAWNER")]
		[SerializeField] private GameObject[] _playerPrefabs;
		[SerializeField] private Transform[] _spawnPoints;
		[Space]
		[SerializeField] private CinemachineVirtualCamera _playerCamera;
		[SerializeField] private GameObject _canvas;

		private GameObject _prefabToSpawn;

		private List<PlayerBrain> _playerBrains = new List<PlayerBrain>();

		public static NetworkManager Instance;

		private GameObject _playerLocal;
		public GameObject PlayerLocal => _playerLocal;

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			_prefabToSpawn = _playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerModel"]];
			var randomSpawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position;

			var player = PhotonNetwork.Instantiate(_prefabToSpawn.name, randomSpawnPoint, Quaternion.identity);
			_playerCamera.Follow = player.transform;
			_playerCamera.LookAt = player.transform;
			_playerLocal = player;

			if (_playerLocal != null)
				_canvas.SetActive(true);

			//Регистрация кастомного типа данных с сериализацией / десериализацией
			//?PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
		}

		public void AddPlayer(PlayerBrain player) 
		{
			_playerBrains.Add(player);
		}

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

