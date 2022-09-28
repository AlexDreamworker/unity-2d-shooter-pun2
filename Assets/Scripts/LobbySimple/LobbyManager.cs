using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterPun2D.pt2
{
	public class LobbyManager : MonoBehaviourPunCallbacks
	{
		[SerializeField] private TMP_Text _logText;
		[SerializeField] private Button _createButton;
		[SerializeField] private Button _joinButton;

		[SerializeField] private TMP_InputField _nickNameInputField;

		//*
		//[SerializeField] private List<PlayerItem> playerItemList = new List<PlayerItem>();
		//[SerializeField] private PlayerItem playerItemPrefab;
		//[SerializeField] private Transform itemParent;
		//* 

		private void Start()
		{
			_createButton.interactable = false;
			_joinButton.interactable = false;
			
			string nickName = PlayerPrefs.GetString("NickName", "Player " + Random.Range(1000, 9999));
			_nickNameInputField.text = nickName;
			PhotonNetwork.NickName = nickName;
			Log("Player's name is set to " + nickName);

			PhotonNetwork.AutomaticallySyncScene = true;
			PhotonNetwork.GameVersion = "1.0";

			if (!PhotonNetwork.IsConnected)
				PhotonNetwork.ConnectUsingSettings();
		}

		public override void OnConnectedToMaster() 
		{
			Log("Connected to Master");
			_createButton.interactable = true;
			_joinButton.interactable = true;
		}

		public void CreateRoom() 
		{
			PhotonNetwork.NickName = _nickNameInputField.text;
			PlayerPrefs.SetString("NickName", _nickNameInputField.text);
			PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 3});
		}

		public void JoinRoom() 
		{
			PhotonNetwork.NickName = _nickNameInputField.text;
			PlayerPrefs.SetString("NickName", _nickNameInputField.text);
			PhotonNetwork.JoinRandomRoom();
		}

		public override void OnJoinedRoom() 
		{
			Log("Joined the room");

			PhotonNetwork.LoadLevel("Game");
		}

		private void Log(string message) 
		{
			Debug.Log(message);
			_logText.text += "\n";
			_logText.text += message;
		}
	}
}

