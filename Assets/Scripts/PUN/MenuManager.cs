using UnityEngine;

namespace ShooterPun2D
{
	public class MenuManager : MonoBehaviour
	{
		public static MenuManager Instance;

		[SerializeField] Menu[] _menus;

		private void Awake()
		{
			Instance = this;
		}

		public void OpenMenu(string menuName)
		{
			for (var i = 0; i < _menus.Length; i++)
			{
				if (_menus[i].MenuName == menuName) 
				{
					_menus[i].Open();
				}
				else if (_menus[i].IsOpen)
				{
					CloseMenu(_menus[i]);
				}
			}
		}

		public void OpenMenu(Menu menu) //* CALL
		{
			for (var i = 0; i < _menus.Length; i++)
			{
				if (_menus[i].IsOpen) 
				{
					CloseMenu(_menus[i]);
				}
			}
			menu.Open();
		}	

		private void CloseMenu(Menu menu)
		{
			menu.Close();
		}		
	}
}

