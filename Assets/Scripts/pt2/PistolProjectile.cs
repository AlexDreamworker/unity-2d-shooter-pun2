using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PistolProjectile : MonoBehaviour
	{		
		[SerializeField] private int _damage = 1;
		private Rigidbody2D _rigidbody;
		private PhotonView _photonView;

		// private void Awake()
		// {
		// }

		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_photonView = GetComponent<PhotonView>();
		}

		// private void Update()
		// {
		// 	if (!_photonView.IsMine)
		// 	{
		// 		Destroy(_rigidbody);
		// 	}
		// }

		private void OnCollisionEnter2D(Collision2D other)
		{
			PhotonView target = other.gameObject.GetComponent<PhotonView>();
			if (target != null && !target.IsMine) 
			{
				target.RPC("TakeDamage", RpcTarget.All, _damage);
				this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.All);
			}	
			
			else if (other.gameObject.CompareTag("Ground"))
			{
				this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.All);
			}							
		}

		[PunRPC]
		private void Destroy() 
		{
			Destroy(gameObject);
		}
	}
}

