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
		[SerializeField] private Camera _camera;

		public void OnMovement(InputAction.CallbackContext context)
		{
			var direction = context.ReadValue<Vector2>().normalized;
			_playerMovement.SetDirection(direction);
		}

		public void OnAim(InputAction.CallbackContext context)
		{
			var direction = context.ReadValue<Vector2>();

			//* IF UNITY EDITOR or STANDALONE WIN
			//* AND ADD TO NewInputSystem -> Aim -> Position[Mouse]
			var mousePosition = _camera.ScreenToWorldPoint((Vector3)direction) - transform.position;
			var roundDirection = Vector2Int.RoundToInt(mousePosition);

			//*IF ANDROID
			//* var directionInput = Vector2Int.RoundToInt(direction);

			var xClamp = Mathf.Clamp(roundDirection.x, -1, 1);
			var yClamp = Mathf.Clamp(roundDirection.y, -1, 1);
			var test = new Vector2(xClamp, yClamp);

			_playerWeapon.SetDirection(test);
		}

		public void OnShoot(InputAction.CallbackContext context)
		{
			var temp = context.performed ? _playerWeapon.IsStartFire = true : _playerWeapon.IsStartFire = false;
		}

		public void OnSwitchWeaponX(InputAction.CallbackContext context)
		{
			if (context.started) 
				_playerWeapon.NextWeapon();			
		}

		public void OnSwitchWeaponY(InputAction.CallbackContext context)
		{
			if (context.started) 
				_playerWeapon.PreviousWeapon();
		}	
	}
}

