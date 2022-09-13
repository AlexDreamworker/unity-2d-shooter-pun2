using UnityEngine;
using UnityEngine.UI; //! Added TMPro!

namespace ShooterPun2D
{
	public class HealthWidget : MonoBehaviour
	{
		[SerializeField] private PlayerHealth _target;
		[SerializeField] private Text _render;

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
		}
	}
}

