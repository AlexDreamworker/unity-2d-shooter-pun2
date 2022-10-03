using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace ShooterPun2D
{
	public class PlayerInfo : MonoBehaviour
	{
		public int _frags;
		private PhotonView _photonView;

		public int Frags => _frags;
		public PhotonView PhotonView => _photonView;

		private Player _player;

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
		}

		public void SetFrags(Player player) 
		{
			_frags++;
			_photonView.RPC(nameof(RpcSetFrags), RpcTarget.All, player); //!!! ???? or ALL
		}

		[PunRPC]
		private void RpcSetFrags(Player player) 
		{
			Hashtable hash = new Hashtable();
			hash.Add("Frags", Frags);
			player.SetCustomProperties(hash);
		}
	}
}

