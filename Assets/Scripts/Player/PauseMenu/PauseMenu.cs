using UnityEngine;

namespace ShooterPun2D
{
	public class PauseMenu : MonoBehaviour
	{
		[SerializeField] private GameObject _menuHolder;
		private bool _isOpen = false;

		private void Start()
		{
			_menuHolder.SetActive(_isOpen);
		}

		public void SwitchPauseMenu()
		{
			_isOpen = !_isOpen;

			_menuHolder.SetActive(_isOpen);
		}
	}
}

