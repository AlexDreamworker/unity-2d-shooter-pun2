using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterPun2D.pt2
{
	public class PauseMenu : MonoBehaviourPunCallbacks
	{
		[SerializeField] private GameObject _menuHolder;
		private bool _isOpen = false;

		private void Start()
		{
			_menuHolder.SetActive(_isOpen);
		}

		public void SwitchPauseMenu()
		{
			_isOpen = !_isOpen;

			_menuHolder.SetActive(_isOpen);
		}

		//* Call from button
		public void Exit() => Application.Quit(); 

		//* Call from button
		public void Leave() => PhotonNetwork.LeaveRoom(); 

		//* Call from button
		public override void OnLeftRoom() => SceneManager.LoadScene(0); 
	}
}

