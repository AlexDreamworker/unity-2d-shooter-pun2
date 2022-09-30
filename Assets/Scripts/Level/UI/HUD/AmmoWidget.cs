using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterPun2D.pt2
{
	public class AmmoWidget : MonoBehaviour
	{
		[SerializeField] private TMP_Text _render;
		[SerializeField] private TMP_Text _textBg;
		[SerializeField] private Image _iconRender;
		
		private PlayerWeapon _target;

		private void OnEnable()
		{
			_target = NetworkManager.Instance.PlayerLocal.GetComponent<PlayerWeapon>();
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
			
			_textBg.text = value.ToString(); //todo: refactoring design
			_render.text = value.ToString();
			_iconRender.color = color;
		}	
	}
}

