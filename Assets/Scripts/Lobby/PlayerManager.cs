using System.IO;
using UnityEngine;
using Photon.Pun;

namespace ShooterPun2D.pt2
{
	public class PlayerManager : MonoBehaviour
	{
		private PhotonView _photonView;

		void Awake()
		{
			_photonView = GetComponent<PhotonView>();
		}

		void Start()
		{
			if (_photonView.IsMine)
				CreateController();
		}

		void CreateController() 
		{
			PhotonNetwork.Instantiate(
				Path.Combine("PhotonPrefabs", "Player"), 
				Vector3.zero, 
				Quaternion.identity
				);
		}
	}
}

