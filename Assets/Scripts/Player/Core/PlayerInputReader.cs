using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPun2D.pt2
{
	public class PlayerInputReader : MonoBehaviour
	{
		private GameObject _pauseMenuCanvas;
		private GameObject _scoreboardWidget;

		private PlayerBrain _playerBrain;

		[SerializeField] private TMP_Text _nickNameText;

		void Awake()
		{
			_playerBrain = GetComponent<PlayerBrain>();
		}

		void Start()
		{
			_nickNameText.text = _playerBrain.PhotonView.Owner.NickName;

			_pauseMenuCanvas = NetworkManager.Instance.PauseMenu;
			_scoreboardWidget = NetworkManager.Instance.ScoreboardMenu;

			_scoreboardWidget.transform.localScale = Vector3.zero;
		}

		public void OnMovement(InputAction.CallbackContext context) 
		{
			if (!_playerBrain.PhotonView.IsMine) 
				return;

			var direction = context.ReadValue<Vector2>();
			_playerBrain.Controls.SetDirectionMove(direction);
		}

		public void OnAim(InputAction.CallbackContext context) 
		{
			if (!_playerBrain.PhotonView.IsMine)
				return;
			
			var direction = context.ReadValue<Vector2>().normalized;
			direction.Normalize();

			var xClamp = ClampingFloatDirection(direction.x);
			var yClamp = ClampingFloatDirection(direction.y);

			var clampDirection = new Vector2(xClamp, yClamp);
			_playerBrain.Controls.SetDirectionAim(clampDirection);
		}

		float ClampingFloatDirection(float value) 
		{
			if (value > 0.55f)
				return 1f;

			if (value < 0.55f && value > 0.25f)
				return 0.5f;
				
			if (value < 0.25f && value > -0.25f)
				return 0;
				
			if (value < -0.25f && value > -0.55f)
				return -0.5f;

			if (value < -0.55f)
				return -1f;
			
			return 0;
		}

		public void OnNextWeapon(InputAction.CallbackContext context) 
		{
			if (!_playerBrain.PhotonView.IsMine)
				return;
			
			if (context.started) 
				_playerBrain.Weapon.NextWeapon();
		}

		public void OnPreviousWeapon(InputAction.CallbackContext context)
		{
			if (!_playerBrain.PhotonView.IsMine)
				return;

			if (context.started) 
				_playerBrain.Weapon.PreviousWeapon();
		}

		public void OnPauseMenu(InputAction.CallbackContext context) 
		{
			if (!_playerBrain.PhotonView.IsMine)
				return; 

			if (context.started)
				_pauseMenuCanvas.GetComponent<PauseMenu>().SwitchPauseMenu();
		}

		public void OnScoreboard(InputAction.CallbackContext context) 
		{
			if (!_playerBrain.PhotonView.IsMine)
				return;
			
			if (context.started)
				_scoreboardWidget.transform.localScale = Vector3.one;
			if (context.canceled)
				_scoreboardWidget.transform.localScale = Vector3.zero;
		}
	}
}

