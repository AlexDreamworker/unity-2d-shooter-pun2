using TMPro;
using UnityEngine;

namespace ShooterPun2D
{
	public class HealthWidget : MonoBehaviour
	{
		[SerializeField] private PlayerHealth _target;
		[SerializeField] private TMP_Text _render;

		private Color color;

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
			
			_render.text = value.ToString();

			var result = value / 100f;

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

