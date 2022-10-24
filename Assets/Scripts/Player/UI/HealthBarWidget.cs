using UnityEngine;
using UnityEngine.UI;

namespace ShooterPun2D.pt2
{
	public class HealthBarWidget : MonoBehaviour
	{
		[SerializeField] private PlayerHealth _target;
		[SerializeField] private Image _healthBar;
		[SerializeField] private Image _damageBar;
		[SerializeField] private Gradient _gradient;

		private const float DAMAGE_HEALTH_SHRINK_TIMER_MAX = 0.5f;
		private float _damageHealthShrinkTimer;
		private float _floatValue;

		void OnEnable()
		{
			_target.OnHealthChanged += UpdateValue;
		}

		void OnDisable()
		{
			_target.OnHealthChanged -= UpdateValue;
		}

		void Update()
		{
			_damageHealthShrinkTimer -= Time.deltaTime;

			if (_damageHealthShrinkTimer < 0)
			{
				if (_healthBar.fillAmount < _damageBar.fillAmount) 
				{
					var shrinkSpeed = 1f;
					_damageBar.fillAmount -= shrinkSpeed * Time.deltaTime;
				}

				if (_healthBar.fillAmount > _damageBar.fillAmount)
					_damageBar.fillAmount = _healthBar.fillAmount;
			}
		}

		public void UpdateValue(int value)
		{
			if (_target == null)
				return;
			
			_damageHealthShrinkTimer = DAMAGE_HEALTH_SHRINK_TIMER_MAX;

			_floatValue = value / 100f;
			_healthBar.fillAmount = _floatValue;
			_healthBar.color = _gradient.Evaluate(_floatValue);		
		}
	}
}

