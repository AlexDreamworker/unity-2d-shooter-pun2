using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace ShooterPun2D.pt2
{
	public class WeaponWidget : MonoBehaviour
	{
		[SerializeField] private GameObject[] _items;
		[SerializeField] private float _visablityDelay = 1f;

		private PlayerWeapon _target;
		private float _tempTimer;

		void OnEnable()
		{
			_target = NetworkManager.Instance.PlayerLocal.GetComponent<PlayerWeapon>();

			_target.OnWeaponChanged += UpdateItems;
			_target.OnWeaponActivated += ActivateItems;
			_target.OnAmmoEmpted += BlockItems;
			_target.OnWeaponRefreshed += DefaultItemsState;
		}

		void OnDisable()
		{
			_target.OnWeaponChanged -= UpdateItems;
			_target.OnWeaponActivated -= ActivateItems;
			_target.OnAmmoEmpted -= BlockItems;
			_target.OnWeaponRefreshed -= DefaultItemsState;
		}

		void Update()
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

		void DefaultItemsState() 
		{
			foreach (var item in _items.Where(i => i != _items[0]))
			{
				item.transform.GetChild(2).gameObject.SetActive(true);
			}
		}

		void UpdateItems(int index) 
		{
			_tempTimer = Time.time + _visablityDelay;
			
			foreach (var item in _items)
				item.transform.GetChild(0).gameObject.SetActive(false);
				
			_items[index].transform.GetChild(0).gameObject.SetActive(true);
		}

		void ActivateItems(int index) 
		{
			_items[index].transform.GetChild(2).gameObject.SetActive(false);
		}

		void BlockItems(int index, bool isEmpty)
		{
			_items[index].transform.GetChild(1).gameObject.SetActive(isEmpty);
		}
	}
}

