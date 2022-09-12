using System;
using UnityEngine;

namespace ShooterPun2D
{
	[Serializable]
	public class Weapon
	{
		[SerializeField] private string _name;
		[SerializeField] private WeaponType _type;
		[SerializeField] private Color _color; //? For testing HUD
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private GameObject _projectilePrefab;
		[SerializeField] private float _projectileSpeed;
		[SerializeField] private float _fireRate;
		[SerializeField] private bool _isActive;
		[SerializeField] private int _ammoCount;

		private int _maxAmmoCount = 666;
		private int _id;

		public WeaponType Type => _type;
		public Color Color => _color; //? For testing HUD
		public Transform ShootPoint => _shootPoint;
		public GameObject ProjectilePrefab => _projectilePrefab;
		public float ProjectileSpeed => _projectileSpeed;
		public float FireRate => _fireRate;

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

