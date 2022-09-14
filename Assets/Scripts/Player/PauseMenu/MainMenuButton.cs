using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterPun2D
{
	public class MainMenuButton : MonoBehaviour
	{
		public void BackToMainMenu()
		{
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.LoadLevel(0);
		}
	}
}

