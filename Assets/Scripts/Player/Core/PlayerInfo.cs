using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace ShooterPun2D
{
	public class PlayerInfo : MonoBehaviour
	{
		public int _frags;

		private PhotonView _photonView;

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
		}

		public void SetFrags() 
		{
			_frags++;
			_photonView.RPC(nameof(RpcSetFrags), RpcTarget.All); //!!! ???? or ALL
		}

		[PunRPC]
		private void RpcSetFrags() 
		{
			Hashtable hash = new Hashtable();
			hash.Add("Frags", _frags);
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		}
	}
}

