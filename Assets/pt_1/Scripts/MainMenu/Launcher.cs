using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

namespace ShooterPun2D.pt1
{
	public class Launcher : MonoBehaviourPunCallbacks
	{
		public static Launcher Instance;

		[SerializeField] private TMP_InputField _roomNameField;
		[SerializeField] private TMP_Text _errorText;
		[SerializeField] private TMP_Text _roomNameText;
		[SerializeField] private Transform _roomListContent;
		[SerializeField] private Transform _playerListContent;
		[SerializeField] private GameObject _roomListItemPrefab;
		[SerializeField] private GameObject _playerListItemPrefab;
		[SerializeField] private GameObject _startGameButton;

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			PhotonNetwork.AutomaticallySyncScene = true;
			MenuManager.Instance.OpenMenu("loading");
			Debug.Log("Connecting to Master");
			PhotonNetwork.ConnectUsingSettings();
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
			//options.PublishUserId = true; //? Check this and other options!
			//options.MaxPlayers = 4; //?
			
			PhotonNetwork.CreateRoom(_roomNameField.text);
			MenuManager.Instance.OpenMenu("loading");
		}

		public override void OnJoinedRoom() 
		{
			MenuManager.Instance.OpenMenu("room");
			_roomNameText.text = PhotonNetwork.CurrentRoom.Name;

			Player[] players = PhotonNetwork.PlayerList;

			foreach (Transform item in _playerListContent)
			{
				Destroy(item.gameObject);
			}
			
			for (var i = 0; i < players.Count(); i++)
			{
				Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
			}

			_startGameButton.SetActive(PhotonNetwork.IsMasterClient);
		}

		public override void OnMasterClientSwitched(Player newMasterClient) 
		{
			_startGameButton.SetActive(PhotonNetwork.IsMasterClient);
		}

		public override void OnCreateRoomFailed(short returnCode, string message) 
		{
			_errorText.text = "Room Created Failed: " + message;
			MenuManager.Instance.OpenMenu("error");
		}

		public void StartGame() 
		{
			if (PhotonNetwork.IsMasterClient)
				PhotonNetwork.LoadLevel(1);
		}

		public void LeaveRoom() //* CALL
		{
			PhotonNetwork.LeaveRoom();
			MenuManager.Instance.OpenMenu("loading");
		}

		public void JoinRoom(RoomInfo info) 
		{
			PhotonNetwork.JoinRoom(info.Name);
			MenuManager.Instance.OpenMenu("loading");
		}

		public override void OnLeftRoom() 
		{
			MenuManager.Instance.OpenMenu("title");
		}

		public override void OnRoomListUpdate(List<RoomInfo> roomList) 
		{
			foreach (Transform item in _roomListContent)
			{
				Destroy(item.gameObject);
			}

			for (int i = 0; i < roomList.Count; i++)
			{
				if (roomList[i].RemovedFromList)
					continue;
					
				Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
			}
		}

		public override void OnPlayerEnteredRoom(Player newPlayer) 
		{
			Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
		}
	}
}

