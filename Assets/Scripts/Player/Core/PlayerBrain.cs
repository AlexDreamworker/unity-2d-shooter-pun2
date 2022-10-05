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
		public PlayerData Data { get; private set; }
		public PhotonView PhotonView { get; private set; }
		public Rigidbody2D Rigidbody { get; private set; }
		public Collider2D Collider { get; private set; }

		[SerializeField] private GameObject _canvasOverhead;
		public GameObject CanvasOverhead => _canvasOverhead;

		public NetworkManager Global { get; private set; }

		private void Awake()
		{
			Input = GetComponent<PlayerInputReader>();
			Controls = GetComponent<PlayerControls>();
			Weapon = GetComponent<PlayerWeapon>();
			Health = GetComponent<PlayerHealth>();
			Graphics = GetComponent<PlayerGraphics>();
			Audio = GetComponent<PlayerAudio>();
			Data = GetComponent<PlayerData>();
			PhotonView = GetComponent<PhotonView>();
			Rigidbody = GetComponent<Rigidbody2D>();
			Collider = GetComponent<Collider2D>();

			Global = null;
		}

		private void Start()
		{
			Global = FindObjectOfType<NetworkManager>();
			Global.AddPlayer(Data);

			if (!PhotonView.IsMine) 
			{
				Destroy(Rigidbody);
			}
		}

		public void ComponentsActivity(bool isActive) 
		{
			Collider.enabled = isActive;
			Input.enabled = isActive;
			Controls.enabled = isActive;
			Weapon.enabled = isActive;
			Graphics.PlayerSpritesActivity(isActive);
			CanvasOverhead.SetActive(isActive);
		}
	}
}

