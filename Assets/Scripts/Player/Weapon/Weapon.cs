using System;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	[Serializable]
	public class Weapon
	{
		[SerializeField] private string _name;
		[SerializeField] private WeaponType _type;
		[SerializeField] private Color _color; 
		[SerializeField] private GameObject _projectilePrefab;
		[SerializeField] private bool _isActive;
		[SerializeField] private int _ammoCount;

		private int _maxAmmoCount = 666;
		private int _id;

		public WeaponType Type => _type;
		public Color Color => _color;
		public GameObject ProjectilePrefab => _projectilePrefab;

		public int Id 
		{
			get => (int)_type;
		}

		public bool IsActive
		{
			get => _isActive;
			set => _isActive = value;
		}

		public int AmmoCount 
		{
			get => _ammoCount;
			set
			{
				_ammoCount = Mathf.Clamp(value, 0, _maxAmmoCount);
			}
		}
	}
}

