using System.Collections;
using UnityEngine;

namespace ShooterPun2D
{
	public class WeaponWidget : MonoBehaviour
	{
		[SerializeField] private PlayerWeapon _target;
		[SerializeField] private GameObject[] _items;
		[SerializeField] private float _visablityDelay = 1f;

		private float _tempTimer;

		private void OnEnable()
		{
			_target.OnWeaponChanged += UpdateItems;
		}

		private void OnDisable()
		{
			_target.OnWeaponChanged -= UpdateItems;
		}

		private void Update()
		{
			var checkTime = Time.time > _tempTimer ? false : true;
			ItemVisability(checkTime);
		}

		public void UpdateItems(int index) 
		{
			if (_target == null)
				return;
			
				_tempTimer = Time.time + _visablityDelay;
			
			foreach (var item in _items)
			{
				item.transform.GetChild(0).gameObject.SetActive(false);
			}
			_items[index].transform.GetChild(0).gameObject.SetActive(true);
		}

		private void ItemVisability(bool isVisable)
		{
			foreach (var item in _items)
			{
				item.SetActive(isVisable);
			}
		}
	}
}

