using Photon.Pun;
using UnityEngine;
using TMPro;

namespace ShooterPun2D
{
	public class Launcher : MonoBehaviourPunCallbacks
	{
		[SerializeField] private TMP_InputField _roomNameField;
		[SerializeField] private TMP_Text _errorText;
		[SerializeField] private TMP_Text _roomNameText;

		private void Start()
		{
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
		}

		public void CreateRoom() //* CALL
		{
			if (string.IsNullOrEmpty(_roomNameField.text))
				return;

			PhotonNetwork.CreateRoom(_roomNameField.text);
			MenuManager.Instance.OpenMenu("loading");
		}

		public override void OnJoinedRoom() 
		{
			MenuManager.Instance.OpenMenu("room");
			_roomNameText.text = PhotonNetwork.CurrentRoom.Name;
		}

		public override void OnCreateRoomFailed(short returnCode, string message) 
		{
			_errorText.text = "Room Created Failed: " + message;
			MenuManager.Instance.OpenMenu("error");
		}

		public void LeaveRoom() //* CALL
		{
			PhotonNetwork.LeaveRoom();
			MenuManager.Instance.OpenMenu("loading");
		}

		public override void OnLeftRoom() 
		{
			MenuManager.Instance.OpenMenu("title");
		}
	}
}

