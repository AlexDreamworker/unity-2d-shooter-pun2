using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPun2D
{
	[RequireComponent(typeof(PlayerMovement))]
	[RequireComponent(typeof(PlayerWeapon))]
	public class PlayerInputReader : MonoBehaviour
	{
		[SerializeField] private PlayerMovement _playerMovement;
		[SerializeField] private PlayerWeapon _playerWeapon;

		public void OnMovement(InputAction.CallbackContext context)
		{
			var direction = context.ReadValue<Vector2>().normalized;
			_playerMovement.SetDirection(direction);
		}

		public void OnAim(InputAction.CallbackContext context)
		{
			var direction = context.ReadValue<Vector2>();

			//* if UNITY EDITOR or STANDALONE WIN
			//* and add to NewInputSystem -> Aim -> Position[Mouse]
			var mouseDirection = Camera.main.ScreenToWorldPoint((Vector3)direction) - transform.position;
			var directionInput = Vector2Int.RoundToInt(mouseDirection);

			//*if ANDROID
			//var directionInput = Vector2Int.RoundToInt(direction);

			var test = new Vector2(Mathf.Clamp(directionInput.x, -1, 1), Mathf.Clamp(directionInput.y, -1, 1));

			_playerWeapon.SetDirection(test);
		}

		public void OnShoot(InputAction.CallbackContext context)
		{
			if (context.started) 
			{
				_playerWeapon.Fire(true);
			}

			if (context.canceled)
			{
				_playerWeapon.Fire(false);
			}
		}

		public void OnSwitchWeaponX(InputAction.CallbackContext context)
		{
			if (context.started) 
			{
				_playerWeapon.NextWeapon();
			}				
		}

		public void OnSwitchWeaponY(InputAction.CallbackContext context)
		{
			if (context.started) 
			{
				_playerWeapon.PreviousWeapon();
			}
		}

		// private float _xVelocity;
		// private Vector2 _aimDirection;

		// public float XVelocity => _xVelocity;
		// public Vector2 AimDirection => _aimDirection;

		// private void Update()
		// {
		// 	PlayerInputHandler();
		// }

		// private void PlayerInputHandler() 
		// {
		// 	_xVelocity = Input.GetAxis("Horizontal");

		// 	Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// 	_aimDirection = mousePosition - (Vector2)_playerWeapon.WeaponHolder.transform.position;

		// 	if (Input.GetKeyDown(KeyCode.Space)) 
		// 		_playerMovement.Jump();
			
		// 	if (Input.GetMouseButton(0)) 
		// 		_playerWeapon.TryFire();
			
		// 	if (Input.GetKeyDown(KeyCode.E)) 
		// 		_playerWeapon.NextWeapon();

		// 	if (Input.GetKeyDown(KeyCode.Q)) 
		// 		_playerWeapon.PreviousWeapon();							
		// }		
	}
}

