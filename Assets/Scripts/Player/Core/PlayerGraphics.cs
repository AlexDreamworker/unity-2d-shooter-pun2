using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerGraphics : MonoBehaviour
	{
		[Header("Reference")]
		[SerializeField] private Animator _animBodyLegs;
		[SerializeField] private Animator _animBodyTorso;
		[SerializeField] private SpriteRenderer _rendererBodyLegs;
		[SerializeField] private SpriteRenderer _rendererBodyTorso;
		[SerializeField] private SpriteRenderer _rendererShootPoint;
		[SerializeField] private GameObject _aimPoint;

		private PlayerBrain _playerBrain;

		private static readonly int IsRunningKey = Animator.StringToHash("is-running");
		private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
		private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
		private static readonly int xValueKey = Animator.StringToHash("x");
		private static readonly int yValueKey = Animator.StringToHash("y");

		private void Awake()
		{
			_playerBrain = GetComponent<PlayerBrain>();	
		}

		private void Start()
		{
			SetAnimatorValueTorso(1, 0);

			if (!_playerBrain.PhotonView.IsMine) 
			{
				_aimPoint.SetActive(false);
			}
		}

		public void SetAnimatorValueLegs(bool isRunning, bool isGrounded, float verticalVelocity) 
		{
			_animBodyLegs.SetBool(IsRunningKey, isRunning);
			_animBodyLegs.SetBool(IsGroundKey, isGrounded);
			_animBodyLegs.SetFloat(VerticalVelocityKey, verticalVelocity); 
		}

		public void SetAnimatorValueTorso(float x, float y) 
		{
			_animBodyTorso.SetFloat(xValueKey, x);
			_animBodyTorso.SetFloat(yValueKey, y);
		}

		public void UpdateSpriteDirectionLegs(float value)
		{
			if (value > 0) 
		 		_rendererBodyLegs.transform.localScale = new Vector3(1, 1, 1);
		 	else if (value < 0) 
		 		_rendererBodyLegs.transform.localScale = new Vector3(-1, 1, 1);
		}

		public void SetShootPointColor(Color color) 
		{
			_rendererShootPoint.color = color;
		}

		public void PlayerSpritesActivity(bool activity) 
		{
			_rendererBodyLegs.enabled = activity;
			_rendererBodyTorso.enabled = activity;
			_rendererShootPoint.enabled = activity;
			_aimPoint.SetActive(activity);

			if (!_playerBrain.PhotonView.IsMine)
				_aimPoint.SetActive(false);
		}
	}
}

