using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterPun2D.pt1
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

