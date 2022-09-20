using UnityEngine;
using UnityEngine.UI;

namespace ShooterPun2D.pt2
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
			
			var result = value / 100f;

			_render.fillAmount = result;

			if (result > 0.7f)
			{
				_render.color = new Color(1 - result, result, 0);
			}
			else
			{
				_render.color = new Color(1, result, 0);
			}
		}
	}
}

