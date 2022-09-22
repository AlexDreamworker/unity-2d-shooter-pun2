using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerBrain : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _bodyLegs;
		[SerializeField] private SpriteRenderer _bodyTorso;
		[SerializeField] private GameObject _canvasOverhead;
		//[SerializeField] private GameObject _bloodParticle = null;
		//[SerializeField] private GameObject _chunkParticle = null;

		[SerializeField] private ParticleSystem _blood;
		[SerializeField] private ParticleSystem _chunk;

		private PhotonView _photonView;
		private Rigidbody2D _rigidbody;
		private Collider2D _collider;
		private PlayerInputReader _playerInput;
		private PlayerMovement _playerMovement;
		private PlayerWeapon _playerWeapon;
		private PlayerHealth _playerHealth;

		private void Awake()
		{
			_photonView = GetComponent<PhotonView>();
			_rigidbody = GetComponent<Rigidbody2D>();
			_collider = GetComponent<Collider2D>();
			_playerInput = GetComponent<PlayerInputReader>();
			_playerMovement = GetComponent<PlayerMovement>();
			_playerWeapon = GetComponent<PlayerWeapon>();
			_playerHealth = GetComponent<PlayerHealth>();
		}

		private void OnEnable()
		{
			//if (!_photonView.IsMine)
				//return;

			//_playerHealth.OnPlayerAlive += Alive();
			//_playerHealth.OnPlayerAlive += Alive;
			_playerHealth.OnPlayerDead += Dying;

			_blood.Stop();
			_chunk.Stop();
		}

		private void OnDisable()
		{
			//if (!_photonView.IsMine)
				//return;
			
			//_playerHealth.OnPlayerAlive -= Alive;
			_playerHealth.OnPlayerDead -= Dying;
		}

		private void Dying()
		{
			_photonView.RPC("Death", RpcTarget.All);
		}

		// private void Alive() 
		// {
		// 	_photonView.RPC("Alived", RpcTarget.All);
		// }
 
		[PunRPC]
		private void Death() 
		{
			//_bloodParticle.SetActive(true);
			//_chunkParticle.SetActive(true);

			_blood.Play();
			_chunk.Play();
			
			if (_photonView.IsMine) 
			{
				_rigidbody.bodyType = RigidbodyType2D.Kinematic;
			}
			_collider.enabled = false;
			_playerInput.enabled = false;
			_playerMovement.enabled = false;
			_playerWeapon.enabled = false;
			_canvasOverhead.SetActive(false);
			_bodyLegs.enabled = false;
			_bodyTorso.enabled = false;
			return;
		}

		// [PunRPC]
		// private void Alived() 
		// {
		// 	//_bloodParticle.SetActive(true);
		// 	//_chunkParticle.SetActive(true);

		// 	_blood.Stop();
		// 	_chunk.Stop();
			
		// 	if (_photonView.IsMine) 
		// 	{
		// 		_rigidbody.bodyType = RigidbodyType2D.Dynamic;
		// 	}
		// 	_collider.enabled = true;
		// 	_playerInput.enabled = true;
		// 	_playerMovement.enabled = true;
		// 	_playerWeapon.enabled = true;
		// 	_canvasOverhead.SetActive(true);
		// 	_bodyLegs.enabled = true;
		// 	_bodyTorso.enabled = true;
		// 	return;
		// }
	}
}

