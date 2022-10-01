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
			
			_target.GetComponent<PlayerBrain>().PlayerIsDead();
			_button.transform.localScale = Vector3.one;
		}	

		public void RespawnButtonCallback() 
		{
			_target.GetComponent<PlayerBrain>().PlayerRespawn();
			_button.transform.localScale = Vector3.zero;
		}
	}
}

