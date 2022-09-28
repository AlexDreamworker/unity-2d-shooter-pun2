using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerModel : MonoBehaviour
	{
		[SerializeField] private GameObject[] _skins;
		private GameObject _currentSkin;

		private int _currentSkinIndex;

		private void Start()
		{
			_currentSkin = _skins[0];
			_currentSkinIndex = 0;
			UpdateSkin();
		}

		public void NextSkin() //* CALL
		{
			_currentSkin = _skins[1];
			_currentSkinIndex = 1;

			UpdateSkin();
		}

		public void PreviousSkin() //* CALL
		{
			_currentSkin = _skins[0];
			_currentSkinIndex = 0;
			
			UpdateSkin();
		}

		private void UpdateSkin() 
		{
			foreach (var skin in _skins)
			{
				skin.SetActive(false);
			}

			_currentSkin.SetActive(true);
			
			PlayerPrefs.SetInt("PlayerModelIndex", _currentSkinIndex);
		}
	}
}

