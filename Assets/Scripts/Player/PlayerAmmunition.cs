using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerAmmunition : MonoBehaviour
	{
		[SerializeField] private int _currentPistolAmmo;
		[SerializeField] private int _currentShotgunAmmo;
		[SerializeField] private int _currentAutomatAmmo;
		[SerializeField] private int _currentRocketAmmo;
		[SerializeField] private int _currentBfgAmmo;

		private int _maxAmmoCount = 999;

		public int PistolAmmo
		{
			get => _currentPistolAmmo;
			set
			{
				_currentPistolAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}

		public int ShotgunAmmo
		{
			get => _currentShotgunAmmo;
			set
			{
				_currentShotgunAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}
		
		public int AutomatAmmo
		{
			get => _currentAutomatAmmo;
			set
			{
				_currentAutomatAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}

		public int RocketAmmo
		{
			get => _currentRocketAmmo;
			set
			{
				_currentRocketAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}

		public int BfgAmmo
		{
			get => _currentBfgAmmo;
			set
			{
				_currentBfgAmmo = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}				
	}
}

