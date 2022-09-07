using UnityEngine;

namespace ShooterPun2D
{
	public class Weapon : MonoBehaviour
	{
		[SerializeField] private GameObject _projectile;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private float _projectileSpeed = 1200f;

		//[SerializeField] private float _fireRate = 8f;

		//private float _readyForNextShoot;

		public void Shoot() 
		{
			GameObject projectile = Instantiate(_projectile, _shootPoint.position, _shootPoint.rotation);
			projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * _projectileSpeed);
		}		
	}
}

