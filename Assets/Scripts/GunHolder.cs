using UnityEngine;

namespace ShooterPun2D
{
	public class GunHolder : MonoBehaviour
	{
		private int _currentWeaponIndex;
		private int _totalWeapon = 1;

		private GameObject[] _guns;
		private GameObject _currentGun;

		public GameObject CurrentGun => _currentGun;

		private void Start()
		{
			_totalWeapon = transform.childCount;
			_guns = new GameObject[_totalWeapon];

			for (int i = 0; i < _totalWeapon; i++) 
			{
				_guns[i] = transform.GetChild(i).gameObject;
				_guns[i].SetActive(false);
			}

			_guns[0].SetActive(true);
			_currentGun = _guns[0];
			_currentWeaponIndex = 0;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.E)) 
			{
				//next weapon
				if (_currentWeaponIndex < _totalWeapon - 1) 
				{
					_guns[_currentWeaponIndex].SetActive(false);
					_currentWeaponIndex ++;
					_guns[_currentWeaponIndex].SetActive(true);
					_currentGun = _guns[_currentWeaponIndex];
				}
			}

			if (Input.GetKeyDown(KeyCode.Q)) 
			{
				//previous weapon
				if (_currentWeaponIndex > 0) 
				{
					_guns[_currentWeaponIndex].SetActive(false);
					_currentWeaponIndex --;
					_guns[_currentWeaponIndex].SetActive(true);
					_currentGun = _guns[_currentWeaponIndex];
				}				
			}
		}

	}
}

