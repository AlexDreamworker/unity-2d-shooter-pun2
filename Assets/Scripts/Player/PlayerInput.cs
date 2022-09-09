using UnityEngine;

namespace ShooterPun2D
{
	[RequireComponent(typeof(PlayerMovement))]
	[RequireComponent(typeof(PlayerWeapon))]
	public class PlayerInput : MonoBehaviour
	{
		[SerializeField] private PlayerMovement _playerMovement;
		[SerializeField] private PlayerWeapon _playerWeapon;

		private float _xVelocity;
		private Vector2 _aimDirection;

		public float XVelocity => _xVelocity;
		public Vector2 AimDirection => _aimDirection;

		private void Update()
		{
			PlayerInputHandler();
		}

		private void PlayerInputHandler() 
		{
			_xVelocity = Input.GetAxis("Horizontal");

			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_aimDirection = mousePosition - (Vector2)_playerWeapon.WeaponHolder.transform.position;

			if (Input.GetKeyDown(KeyCode.Space)) 
				_playerMovement.Jump();
			
			if (Input.GetMouseButton(0)) 
				_playerWeapon.TryFire();
			
			if (Input.GetKeyDown(KeyCode.E)) 
				_playerWeapon.NextWeapon();

			if (Input.GetKeyDown(KeyCode.Q)) 
				_playerWeapon.PreviousWeapon();							
		}		
	}
}

