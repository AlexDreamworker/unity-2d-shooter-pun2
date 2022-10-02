using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class Respawn : MonoBehaviour
	{
		[SerializeField] private GameObject _button;
		
		private PlayerHealth _target;

		private void OnEnable()
		{
			_button.transform.localScale = Vector3.zero;

			_target = NetworkManager.Instance.PlayerLocal.GetComponent<PlayerHealth>();
			_target.OnPlayerDead += PlayerDead;
		}

		private void OnDisable()
		{
			_target.OnPlayerDead -= PlayerDead;
		}	

		private void PlayerDead()
		{
			if (_target == null)
				return;
			
			_button.transform.localScale = Vector3.one;
		}	

		public void RespawnButtonCallback() //* Callback from button
		{
			_target.ChangePlayerState(true);
			_button.transform.localScale = Vector3.zero;
		}
	}
}

