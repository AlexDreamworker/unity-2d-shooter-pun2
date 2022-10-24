using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections.Generic;

namespace ShooterPun2D.pt2
{
	public class NetworkManager : MonoBehaviourPunCallbacks
	{
		[Header("---Spawner---")]
		[SerializeField] private GameObject[] _playerPrefabs;
		[SerializeField] private Transform[] _spawnPoints;
		[Header("---Components---")]
		[SerializeField] private CinemachineVirtualCamera _playerCamera;
		[SerializeField] private GameObject _canvas;
		[SerializeField] private GameObject _pauseMenu;
		[SerializeField] private GameObject _scoreboardMenu;

		private GameObject _prefabToSpawn;
		private GameObject _playerLocal;

		private List<PlayerData> _players = new List<PlayerData>();

		public static NetworkManager Instance;

		public GameObject PlayerLocal => _playerLocal;
		public GameObject PauseMenu => _pauseMenu;
		public GameObject ScoreboardMenu => _scoreboardMenu;

		void Awake()
		{
			Instance = this;
			_canvas.SetActive(false);
		}

		void Start()
		{
			_prefabToSpawn = _playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerModel"]];

			var player = PhotonNetwork.Instantiate(_prefabToSpawn.name, GetSpawnPoint(), Quaternion.identity);
			_playerCamera.Follow = player.transform;
			_playerCamera.LookAt = player.transform;
			_playerLocal = player;

			if (_playerLocal != null) 
				_canvas.SetActive(true);
		}

		public Vector3 GetSpawnPoint() 
		{
			var result = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position;
			return result;
		}

		public void AddPlayer(PlayerData info) 
		{
			_players.Add(info);
		}

		public void Leave() => PhotonNetwork.LeaveRoom();

		public override void OnLeftRoom() => SceneManager.LoadScene(0);

		public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) 
		{ 
			Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
		}

		public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) 
		{
			Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
		}
    }
}

