using UnityEngine;

namespace ShooterPun2D
{
	public class PlayerShoot : MonoBehaviour
	{
		[SerializeField] private Transform _gunHolder;
		[SerializeField] private GameObject _projectile;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private float _projectileSpeed = 1200f;
		[SerializeField] private float _fireRate = 8f;

		private float _readyForNextShoot;

		private Vector2 _direction;

		private void Update()
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_direction = mousePosition - (Vector2)_gunHolder.position;

			FaceMouse();

			if (Input.GetMouseButton(0)) 
			{
				if (Time.time > _readyForNextShoot) 
				{
					_readyForNextShoot = Time.time + 1 / _fireRate;
					Shoot();
				}
			}
		}

		private void FaceMouse() 
		{
			_gunHolder.transform.right = _direction;
		}

		private void Shoot() 
		{
			GameObject projectile = Instantiate(_projectile, _shootPoint.position, _shootPoint.rotation);
			projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * _projectileSpeed);
		}
	}
}

