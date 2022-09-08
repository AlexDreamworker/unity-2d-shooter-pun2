using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerHealth : MonoBehaviour
	{
		[SerializeField] private int _currentHealth = 100;
		private int _maxHealth = 100;

		public void Heal(int amount) 
		{
			_currentHealth += amount;
			_currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
		}
	}
}

