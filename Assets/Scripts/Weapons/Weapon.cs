using UnityEngine;

namespace ShooterPun2D
{
	public class Weapon : MonoBehaviour
	{
		[SerializeField] protected PlayerAmmunition _ammunition;
		[SerializeField] private GameObject _projectile;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private float _projectileSpeed;

		[SerializeField] private float _fireRate;

		public float FireRate => _fireRate;

		public virtual void Shoot() 
		{
			GameObject projectile = Instantiate(_projectile, _shootPoint.position, _shootPoint.rotation);
			projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * _projectileSpeed);
		}		
	}
}

