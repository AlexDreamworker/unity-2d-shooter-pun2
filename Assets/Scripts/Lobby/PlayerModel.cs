using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerModel : MonoBehaviour //! 60
	{
		[SerializeField] private GameObject[] _skins;
		private GameObject _currentSkin;

		private int _currentSkinIndex;

		private void Start()
		{
			_currentSkinIndex = PlayerPrefs.GetInt("PlayerModelIndex", 0);
			UpdateSkin();
		}


		public void NextSkin() //* CALL
		{
			if (_currentSkinIndex < _skins.Length - 1)
				_currentSkinIndex++;
			else 
				_currentSkinIndex = 0;

			UpdateSkin();
		}

		public void PreviousSkin() //* CALL
		{
			if (_currentSkinIndex > 0)
				_currentSkinIndex--;
			else 
				_currentSkinIndex = _skins.Length - 1;
			
			UpdateSkin();
		}

		private void UpdateSkin() 
		{
			foreach (var skin in _skins)
			{
				skin.SetActive(false);
			}

			_currentSkin = _skins[_currentSkinIndex];
			_currentSkin.SetActive(true);
			
			PlayerPrefs.SetInt("PlayerModelIndex", _currentSkinIndex);
		}
	}
}

