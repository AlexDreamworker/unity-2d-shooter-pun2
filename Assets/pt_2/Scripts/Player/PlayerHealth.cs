using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerHealth : MonoBehaviour
	{
		[SerializeField] private int _health = 100;
		private PhotonView _photonView;

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
		}

		public void TakeDamage(int value) 
		{
			//if (!_photonView.IsMine)
				_photonView.RPC("Damage", RpcTarget.All, value);
		}

		[PunRPC]
		private void Damage(int value) 
		{
			_health -= value;
		}
	}
}

