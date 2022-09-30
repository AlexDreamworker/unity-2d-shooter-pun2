using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerState : MonoBehaviourPun
	{
		// [SerializeField] private SpriteRenderer _bodyLegs;
		// [SerializeField] private SpriteRenderer _bodyTorso;
		// [SerializeField] private GameObject _canvasOverhead;

		// [SerializeField] private ParticleSystem _blood;
		// [SerializeField] private ParticleSystem _chunk;

		// [SerializeField] private GameObject _respawnButtonHolder = null; //todo: refact

		// private PhotonView _photonView;
		// private Rigidbody2D _rigidbody;
		// private Collider2D _collider;
		// private PlayerInputReader _playerInput;
		// private PlayerControls _playerMovement;
		// private PlayerWeapon _playerWeapon;
		// private PlayerHealth _playerHealth;
		// private PlayerInfo _playerInfo;

		// private void Awake()
		// {
		// 	_photonView = GetComponent<PhotonView>();
		// 	_rigidbody = GetComponent<Rigidbody2D>();
		// 	_collider = GetComponent<Collider2D>();
		// 	_playerInput = GetComponent<PlayerInputReader>();
		// 	_playerMovement = GetComponent<PlayerControls>();
		// 	_playerWeapon = GetComponent<PlayerWeapon>();
		// 	_playerHealth = GetComponent<PlayerHealth>();
		// 	_playerInfo = GetComponent<PlayerInfo>();
		// }

		// private void OnEnable()
		// {
		// 	_playerHealth.OnPlayerDead += Dying;

		// 	_blood.Stop();
		// 	_chunk.Stop();
		// }

		// private void OnDisable()
		// {
		// 	_playerHealth.OnPlayerDead -= Dying;
		// }

		// private void Dying()
		// {

		// 	if (!_photonView.IsMine)
		// 		_playerInfo.SetFrags();
				
		// 	_photonView.RPC(nameof(RpcDeath), RpcTarget.All);
		// }

		// [PunRPC]
		// private void RpcDeath() 
		// {
		// 	_blood.Play();
		// 	_chunk.Play();
			
		// 	if (_photonView.IsMine) 
		// 	{
		// 		_rigidbody.bodyType = RigidbodyType2D.Kinematic;
		// 		_rigidbody.velocity = Vector2.zero;
		// 		_respawnButtonHolder.SetActive(true);
		// 	}
		// 	_collider.enabled = false;
		// 	_playerInput.enabled = false;
		// 	_playerMovement.enabled = false;
		// 	_playerWeapon.enabled = false;
		// 	//?_playerWeapon.ShootPointColorActivity(false);
		// 	_canvasOverhead.SetActive(false);
		// 	_bodyLegs.enabled = false;
		// 	_bodyTorso.enabled = false;
		// 	return;
		// }

		// public void PlayerRespawn() //* CALL 
		// {
		// 	_photonView.RPC(nameof(RpcRespawn), RpcTarget.All);
		// }

		// [PunRPC]
		// private void RpcRespawn() 
		// {
		// 	_blood.Stop();
		// 	_chunk.Stop();

		// 	_blood.Clear();
		// 	_chunk.Clear();
			
		// 	Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f));
		// 	gameObject.transform.position = randomPosition;
			
		// 	if (_photonView.IsMine) 
		// 	{
		// 		_rigidbody.bodyType = RigidbodyType2D.Dynamic;
		// 		_respawnButtonHolder.SetActive(false);
		// 	}
		// 	_collider.enabled = true;
		// 	_playerInput.enabled = true;
		// 	_playerMovement.enabled = true;
		// 	_playerWeapon.enabled = true;
		// 	//?_playerWeapon.ShootPointColorActivity(true);
		// 	_canvasOverhead.SetActive(true);
		// 	_bodyLegs.enabled = true;
		// 	_bodyTorso.enabled = true;

		// 	//?_playerWeapon.SetAimAnimation();
		// 	_playerWeapon.SetWeaponOnStart();
		// 	_playerHealth.TakeDamage(100);
		// 	return;
		// }
	}
}

