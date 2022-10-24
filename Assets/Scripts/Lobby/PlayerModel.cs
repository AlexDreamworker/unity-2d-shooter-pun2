using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerModel : MonoBehaviour 
	{
		[SerializeField] private GameObject[] _skins;
		private GameObject _currentSkin;

		private int _currentSkinIndex;
		private readonly string _prefsPlayerModelIndex = "PlayerModelIndex";

		void Start()
		{
			_currentSkinIndex = PlayerPrefs.GetInt(_prefsPlayerModelIndex, 0);
			UpdateSkin();
		}


		public void NextSkin()
		{
			if (_currentSkinIndex < _skins.Length - 1)
				_currentSkinIndex++;
			else 
				_currentSkinIndex = 0;

			UpdateSkin();
		}

		public void PreviousSkin()
		{
			if (_currentSkinIndex > 0)
				_currentSkinIndex--;
			else 
				_currentSkinIndex = _skins.Length - 1;
			
			UpdateSkin();
		}

		void UpdateSkin() 
		{
			foreach (var skin in _skins)
				skin.SetActive(false);

			_currentSkin = _skins[_currentSkinIndex];
			_currentSkin.SetActive(true);
			
			PlayerPrefs.SetInt(_prefsPlayerModelIndex, _currentSkinIndex);
		}
	}
}

