using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerBrain : MonoBehaviour
	{
		public PlayerInputReader Input { get; private set; }
		public PlayerControls Controls { get; private set; }
		public PlayerWeapon Weapon { get; private set; }
		public PlayerHealth Health { get; private set; }
		public PlayerGraphics Graphics { get; private set; }
		public PlayerAudio Audio { get; private set; }
		public PhotonView PhotonView { get; private set; }
		public Rigidbody2D Rigidbody { get; private set; }
		public Collider2D Collider { get; private set; }

		[SerializeField] private GameObject _canvasOverHead;
		public GameObject CanvasOverhead => _canvasOverHead;

		public NetworkManager Global { get; private set; }

		private void Awake()
		{
			Input = GetComponent<PlayerInputReader>();
			Controls = GetComponent<PlayerControls>();
			Weapon = GetComponent<PlayerWeapon>();
			Health = GetComponent<PlayerHealth>();
			Graphics = GetComponent<PlayerGraphics>();
			Audio = GetComponent<PlayerAudio>();
			PhotonView = GetComponent<PhotonView>();
			Rigidbody = GetComponent<Rigidbody2D>();
			Collider = GetComponent<Collider2D>();

			Global = null;
		}

		private void Start()
		{
			Global = FindObjectOfType<NetworkManager>();
			Global.AddPlayer(this.gameObject);

			if (!PhotonView.IsMine) 
			{
				Destroy(Rigidbody);
			}
		}

		//!---------------------------------------------------------------------------
		public void PlayerIsDead()
		{
			//Global.EnableRespawnButton(this.gameObject);
			PhotonView.RPC(nameof(RpcPlayerIsDead), RpcTarget.All);
		}

		public void PlayerRespawn() 
		{
			PhotonView.RPC(nameof(RpcPlayerRespawn), RpcTarget.All);
		}

		[PunRPC]
		public void RpcPlayerIsDead() 
		{
			//_blood.Play();
			//_chunk.Play();
			
			if (PhotonView.IsMine) 
			{
				Rigidbody.bodyType = RigidbodyType2D.Kinematic;
				Rigidbody.velocity = Vector2.zero;
				//_respawnButtonHolder.SetActive(true);
			}
			Collider.enabled = false;
			Input.enabled = false;
			Controls.enabled = false;
			Weapon.enabled = false;
			Graphics.PlayerSpritesActivity(false);
			_canvasOverHead.SetActive(false);

			//?_playerWeapon.ShootPointColorActivity(false);
			//_canvasOverhead.SetActive(false);
			//_bodyLegs.enabled = false;
			//_bodyTorso.enabled = false;
			return;
		}

		[PunRPC]
		public void RpcPlayerRespawn() 
		{
			//_blood.Stop();
			//_chunk.Stop();

			//_blood.Clear();
			//_chunk.Clear();
			
			Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f));
			gameObject.transform.position = randomPosition;
			
			if (PhotonView.IsMine) 
			{
				Rigidbody.bodyType = RigidbodyType2D.Dynamic;
				//_respawnButtonHolder.SetActive(false);
			}
			Collider.enabled = true;
			Input.enabled = true;
			Controls.enabled = true;
			Weapon.enabled = true;
			Graphics.PlayerSpritesActivity(true);
			_canvasOverHead.SetActive(true);

			//?_playerWeapon.ShootPointColorActivity(true);
			//_canvasOverhead.SetActive(true);
			//_bodyLegs.enabled = true;
			//_bodyTorso.enabled = true;

			//?_playerWeapon.SetAimAnimation();
			Weapon.SetWeaponOnStart();
			Health.TakeDamage(100);
			return;
		}
		//!---------------------------------------------------------------------------
	}
}

