using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class Menu : MonoBehaviour
	{
		[SerializeField] private string _menuName;
		[SerializeField] private bool _isOpen;

		public string MenuName => _menuName;
		public bool IsOpen => _isOpen;

		public void Open() 
		{
			_isOpen = true;
			gameObject.SetActive(true);
		}

		public void Close() 
		{
			_isOpen = false;
			gameObject.SetActive(false);
		}
	}
}

