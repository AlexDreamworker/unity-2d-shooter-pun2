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
		}

		private void Start()
		{
			FindObjectOfType<NetworkManager>().AddPlayer(this);

			if (!PhotonView.IsMine) 
			{
				Destroy(Rigidbody);
			}
		}
	}
}

