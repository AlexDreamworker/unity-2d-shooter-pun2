using UnityEngine;
using DG.Tweening;

namespace ShooterPun2D.pt2
{
	public class WeaponWidget : MonoBehaviour
	{
		[SerializeField] private PlayerWeapon _target;
		[SerializeField] private GameObject[] _items;
		[SerializeField] private float _visablityDelay = 1f;

		private float _tempTimer;

		private void OnEnable()
		{
			//_target.OnWeaponChanged += UpdateItems;
		}

		private void OnDisable()
		{
			//_target.OnWeaponChanged -= UpdateItems;
		}

		private void Update()
		{
			if (Time.time > _tempTimer)
			{
				transform.DOScale(new Vector3(1,0,1), 0.3f);
				return;
			}
			else 
			{
				transform.DOScale(new Vector3(1,1,1), 0.3f);
				return;
			}
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
	}
}

