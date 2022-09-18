using System.IO;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

namespace ShooterPun2D.pt1
{
	public class PlayerManager : MonoBehaviour
	{
		private PhotonView _photonView;

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			if (_photonView.IsMine)
				CreateController();
		}

		private void CreateController() 
		{
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity);
		}
	}
}

