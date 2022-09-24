using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterPun2D.pt2
{
	public class AmmoWidget : MonoBehaviour
	{
		[SerializeField] private PlayerWeapon _target;
		[SerializeField] private TMP_Text _render;
		[SerializeField] private Image _iconRender;

		private void OnEnable()
		{
			_target.OnAmmoChanged += UpdateValue;
		}

		private void OnDisable()
		{
			_target.OnAmmoChanged -= UpdateValue;
		}	

		public void UpdateValue(int value, Color color)
		{
			if (_target == null)
				return;
			
			_render.text = value.ToString();
			_iconRender.color = color;
		}	
	}
}

