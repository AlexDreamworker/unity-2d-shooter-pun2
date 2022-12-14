using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

namespace ShooterPun2D.pt2
{
	public class Launcher : MonoBehaviourPunCallbacks
	{
		[Header("Title Menu")]
		[SerializeField] private TMP_InputField _nickNameInputField;
		[Header("Create Room Menu")]
		[SerializeField] private TMP_InputField _roomNameField;
		[Header("Room Menu")]
		[SerializeField] private TMP_Text _roomNameText;
		[SerializeField] private Transform _playerListContent;
		[SerializeField] private GameObject _playerListItemPrefab;
		[SerializeField] private GameObject _startGameButton;
		[Header("Error Menu")]
		[SerializeField] private TMP_Text _errorText;
		[Header("Find Room Menu")]
		[SerializeField] private Transform _roomListContent;
		[SerializeField] private GameObject _roomListItemPrefab;

		public static Launcher Instance;
		private ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();

		void Awake() => Instance = this;

		void Start()
		{
			string nickName = PlayerPrefs.GetString("NickName", "Player " + Random.Range(1000, 9999));
			_nickNameInputField.text = nickName;
			PhotonNetwork.NickName = nickName;

			PhotonNetwork.AutomaticallySyncScene = true;
			PhotonNetwork.GameVersion = "1.0";

			MenuManager.Instance.OpenMenu("loading");
			Debug.Log("Connecting to Master");
			
			if (!PhotonNetwork.IsConnected)
				PhotonNetwork.ConnectUsingSettings();

			_roomNameField.text = "Test Room 101";
		}

		public override void OnConnectedToMaster()
		{
			Debug.Log("Connected to Master");
			PhotonNetwork.JoinLobby();
		}

		public override void OnJoinedLobby()
		{
			MenuManager.Instance.OpenMenu("title");
			Debug.Log("Joined Lobby");
			PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
		}

		public void CreateRoom() //* CALL
		{
			if (string.IsNullOrEmpty(_roomNameField.text))
				return;

			RoomOptions options = new RoomOptions();
			options.BroadcastPropsChangeToAll = true;

			PhotonNetwork.NickName = _nickNameInputField.text;
			PlayerPrefs.SetString("NickName", _nickNameInputField.text);
			PhotonNetwork.CreateRoom(_roomNameField.text, new RoomOptions { MaxPlayers = 8, BroadcastPropsChangeToAll = true}); //todo: ?????broadcast
			
			MenuManager.Instance.OpenMenu("loading");

			_playerProperties["playerModel"] = PlayerPrefs.GetInt("PlayerModelIndex");
			PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
		}

		public override void OnJoinedRoom() 
		{
			MenuManager.Instance.OpenMenu("room");
			_roomNameText.text = PhotonNetwork.CurrentRoom.Name;

            Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

			foreach (Transform item in _playerListContent)
				Destroy(item.gameObject);
			
			for (var i = 0; i < players.Count(); i++)
				Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);

			_startGameButton.SetActive(PhotonNetwork.IsMasterClient);
		}

		public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient) 
		{
			_startGameButton.SetActive(PhotonNetwork.IsMasterClient);
		}

		public override void OnCreateRoomFailed(short returnCode, string message) 
		{
			_errorText.text = "Room Created Failed: " + message;
			MenuManager.Instance.OpenMenu("error");
		}

		public void StartGame() //* CALL
		{			
			if (PhotonNetwork.IsMasterClient)
				PhotonNetwork.LoadLevel(PlayerPrefs.GetInt("RoomModelIndex", 1));
		}

		public void LeaveRoom() //* CALL
		{
			PhotonNetwork.LeaveRoom();
			MenuManager.Instance.OpenMenu("loading");
		}

		public void JoinRoom(RoomInfo info) //* CALL
		{
			PhotonNetwork.NickName = _nickNameInputField.text;
			PlayerPrefs.SetString("NickName", _nickNameInputField.text);

			PhotonNetwork.JoinRoom(info.Name);
			MenuManager.Instance.OpenMenu("loading");

			_playerProperties["playerModel"] = PlayerPrefs.GetInt("PlayerModelIndex");
			PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
		}

		public void Exit()
		{
			Application.Quit();
		}

		public override void OnLeftRoom() 
		{
			MenuManager.Instance.OpenMenu("title");
		}

		public override void OnRoomListUpdate(List<RoomInfo> roomList) 
		{
			foreach (Transform item in _roomListContent)
				Destroy(item.gameObject);

			for (int i = 0; i < roomList.Count; i++)
			{
				if (roomList[i].RemovedFromList)
					continue;
					
				Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
			}
		}

		public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) 
		{
			Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
		}
	}
}

