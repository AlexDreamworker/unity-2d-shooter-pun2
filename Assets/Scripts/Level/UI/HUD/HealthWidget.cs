using TMPro;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class HealthWidget : MonoBehaviour
	{
		[SerializeField] private TMP_Text _render;
		[SerializeField] private TMP_Text _textBg;
		
		private PlayerHealth _target;

		void OnEnable()
		{
			_target = NetworkManager.Instance.PlayerLocal.GetComponent<PlayerHealth>();
			_target.OnHealthChanged += UpdateValue;
		}

		void OnDisable()
		{
			_target.OnHealthChanged -= UpdateValue;
		}

		public void UpdateValue(int value)
		{
			if (_target == null)
				return;
			
			_render.text = value.ToString();
			_textBg.text = value.ToString();

			var result = value / 100f;

			if (result > 0.7f)
				_render.color = new Color(1 - result, result, 0);
			else
				_render.color = new Color(1, result, 0);
		}
	}
}

