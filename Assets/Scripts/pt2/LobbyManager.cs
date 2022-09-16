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

		private void Start()
		{
			_createButton.interactable = false;
			_joinButton.interactable = false;
			
			PhotonNetwork.NickName = "Player " + Random.Range(1000, 9999);
			Log("Player's name is set to " + PhotonNetwork.NickName);

			PhotonNetwork.AutomaticallySyncScene = true;
			PhotonNetwork.GameVersion = "1.0";
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
			PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 3});
		}

		public void JoinRoom() 
		{
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

