using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterPun2D.pt2
{
	public class PlayerInputReader : MonoBehaviour
	{
		[SerializeField] private Camera _camera; //todo: refactoring!
		[SerializeField] private GameObject _inputCanvas; //todo: refactoring!
		[SerializeField] private GameObject _pauseMenuCanvas; //todo: refactoring!
		[SerializeField] private GameObject _scoreboardWidget;
		private PlayerMovement _playerMovement;
		private PlayerWeapon _playerWeapon;
		private PhotonView _photonView;

		[SerializeField] private TMP_Text _nickNameText; //todo: refactoring!

		private void Awake()
		{
			_playerMovement = GetComponent<PlayerMovement>();
			_playerWeapon = GetComponent<PlayerWeapon>();
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			if (!_photonView.IsMine) 
			{
				Destroy(_camera);
				Destroy(_inputCanvas);
				Destroy(_pauseMenuCanvas);
			}

			_nickNameText.text = _photonView.Owner.NickName; //todo: refactoring!
		}

		public void OnMovement(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine) 
				return;

			var direction = context.ReadValue<Vector2>();
			_playerMovement.SetDirection(direction);
		}

		public void OnAim(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine)
				return;
			
			var direction = context.ReadValue<Vector2>();
			var roundDirection = Vector2Int.RoundToInt(direction);

			_playerWeapon.SetDirection(roundDirection);
		}

		public void OnNextWeapon(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine)
				return;
			
			if (context.started) 
				_playerWeapon.NextWeapon();
		}

		public void OnPreviousWeapon(InputAction.CallbackContext context)
		{
			if (!_photonView.IsMine)
				return;

			if (context.started) 
				_playerWeapon.PreviousWeapon();
		}

		public void OnPauseMenu(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine)
				return; 

			if (context.started)
			{
				_pauseMenuCanvas.GetComponent<PauseMenu>().SwitchPauseMenu();
			}
		}

		public void OnScoreboard(InputAction.CallbackContext context) 
		{
			if (!_photonView.IsMine)
				return;
			
			if (context.started)
				_scoreboardWidget.SetActive(true);
			if (context.canceled)
				_scoreboardWidget.SetActive(false);
		}
	}
}

