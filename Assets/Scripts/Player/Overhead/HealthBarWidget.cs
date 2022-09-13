using UnityEngine;
using UnityEngine.UI;

namespace ShooterPun2D
{
	public class HealthBarWidget : MonoBehaviour
	{
		[SerializeField] private PlayerHealth _target;
		[SerializeField] private Image _render;

		private void OnEnable()
		{
			_target.OnHealthChanged += UpdateValue;
		}

		private void OnDisable()
		{
			_target.OnHealthChanged -= UpdateValue;
		}

		public void UpdateValue(int value)
		{
			if (_target == null)
				return;
			
			_render.fillAmount = value / 100f;
		}
	}
}

