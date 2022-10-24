using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class Respawn : MonoBehaviour
	{
		[SerializeField] private GameObject _button;
		
		private PlayerHealth _target;

		void OnEnable()
		{
			_button.transform.localScale = Vector3.zero;

			_target = NetworkManager.Instance.PlayerLocal.GetComponent<PlayerHealth>();
			_target.OnPlayerDead += PlayerDead;
		}

		void OnDisable()
		{
			_target.OnPlayerDead -= PlayerDead;
		}	

		void PlayerDead()
		{
			if (_target == null)
				return;
			
			_button.transform.localScale = Vector3.one;
		}	

		void RespawnButtonCallback()
		{
			_target.ChangePlayerState(true);
			_button.transform.localScale = Vector3.zero;
		}
	}
}

