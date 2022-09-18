using Photon.Pun;
using UnityEngine;

namespace ShooterPun2D.pt2
{
	public class PlayerMovement : MonoBehaviour, IPunObservable
	{
		[SerializeField] private Animator _bodyLegsAnim;
		[SerializeField] private SpriteRenderer _rendererLegs;
		[SerializeField] private float _speed;
		private Vector2 _direction;
		private Rigidbody2D _rigidbody;
		private PhotonView _photonView;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			if (!_photonView.IsMine) 
			{
				Destroy(_rigidbody);
			}
		}

		private void Update()
		{
			UpdateSpriteDirection();
		}

		private void FixedUpdate()
		{			
			UpdateMovement();
		}

		private void UpdateMovement() 
		{
			var xVelocity = _direction.x * _speed;
			var yVelocity = _direction.y * _speed;

			if (_photonView.IsMine) 
			{
				_rigidbody.velocity = new Vector2(xVelocity, yVelocity);
			}

			_bodyLegsAnim.SetBool("is-running", _direction.x != 0);
		}

		private void UpdateSpriteDirection() 
		{
			if (_direction.x > 0) 
				_rendererLegs.transform.localScale = new Vector3(1, 1, 1);
			else if (_direction.x < 0) 
				_rendererLegs.transform.localScale = new Vector3(-1, 1, 1);
		}

		public void SetDirection(Vector2 direction) 
		{
			_direction = direction;
		}

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
			if (stream.IsWriting)
			{
				stream.SendNext(_direction);
			}
			else 
			{
				_direction = (Vector2)stream.ReceiveNext();
			}
        }
    }
}

