using System;
using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerHealth : MonoBehaviour
	{
		public event Action<int> OnHealthChanged;

		[SerializeField] private int _currentHealth = 100;
		private int _maxHealth = 100;

		private void Start()
		{
			OnHealthChanged?.Invoke(_currentHealth);
		}

		public void Heal(int amount) 
		{
			_currentHealth += amount;
			_currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
			
			OnHealthChanged?.Invoke(_currentHealth);
		}
	}
}

