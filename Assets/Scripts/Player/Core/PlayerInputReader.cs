using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPun2D.pt2
{
	public class PlayerInputReader : MonoBehaviour
	{
		private GameObject _pauseMenuCanvas; //TODO: refactoring
		private GameObject _scoreboardWidget;

		private PlayerBrain _playerBrain;

		[SerializeField] private TMP_Text _nickNameText; //TODO: refactoring

		private void Awake()
		{
			_playerBrain = GetComponent<PlayerBrain>();
		}

		private void Start()
		{
			_nickNameText.text = _playerBrain.PhotonView.Owner.NickName; //TODO: refactoring

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
			//* --- OLD RESULT: ---
			//*
			//*var roundDirection = Vector2Int.RoundToInt(direction);
			//*_playerBrain.Controls.SetDirectionAim(roundDirection);

			//TODO: refactoring!!!!
			//!---------------------------------------------------------------------------------
			var xClamp = direction.x;
			var yClamp = direction.y;
			
			//? ---X---
			if (xClamp > 0.55f)
				xClamp = 1f;

			if (xClamp < 0.55f && xClamp > 0.25f)
				xClamp = 0.5f;
				
			if (xClamp < 0.25f && xClamp > -0.25f)
				xClamp = 0;
				
			if (xClamp < -0.25f && xClamp > -0.55f)
				xClamp = -0.5f;

			if (xClamp < -0.55f)
				xClamp = -1f;

			//? ---Y---
			if (yClamp > 0.55f)
				yClamp = 1f;

			if (yClamp < 0.55f && yClamp > 0.25f)
				yClamp = 0.5f;
				
			if (yClamp < 0.25f && yClamp > -0.25f)
				yClamp = 0;
				
			if (yClamp < -0.25f && yClamp > -0.55f)
				yClamp = -0.5f;

			if (yClamp < -0.55f)
				yClamp = -1f;


			var clampDirection = new Vector2(xClamp, yClamp);
			_playerBrain.Controls.SetDirectionAim(clampDirection);
			//!---------------------------------------------------------------------------------
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

