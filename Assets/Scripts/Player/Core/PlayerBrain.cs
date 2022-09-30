using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerBrain : MonoBehaviour
	{
		private PlayerInputReader _inputHandler;
		private PlayerMovement _movement;
		private PlayerWeapon _weapon;
		private PlayerHealth _health;

		public PlayerInputReader InputReader => _inputHandler;
		public PlayerMovement Movement => _movement;
		public PlayerWeapon Weapon => _weapon;
		public PlayerHealth Health => _health;

		private void Awake()
		{
			_inputHandler = GetComponent<PlayerInputReader>();
			_movement = GetComponent<PlayerMovement>();
			_weapon = GetComponent<PlayerWeapon>();
			_health = GetComponent<PlayerHealth>();
		}

		private void Start()
		{
			FindObjectOfType<NetworkManager>().AddPlayer(this);
		}
	}
}

