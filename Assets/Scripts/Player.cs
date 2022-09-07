using UnityEngine;

namespace ShooterPun2D
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class Player : MonoBehaviour
	{
		[SerializeField] private float _speed = 8f;
		[SerializeField] private float _jumpForce = 500f;
		[SerializeField] private GameObject _weaponHolder;

		private Rigidbody2D _rigidbody;
		private Collider2D _collider;
		private PhysicsMaterial2D _playerMaterial;

		//controller
		private float xVelocity;
		private Vector2 _direction;

		//weapons
		private float _timeToShoot;
		private int _currentWeaponIndex;
		private int _weaponsCount;
		private GameObject[] _weapons;
		private GameObject _currentWeapon;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_collider = GetComponent<Collider2D>();

			CreatePhysicsMaterial();
		}

		private void Start()
		{
			SetDefaultWeaponOnStart();
		}	

		private void Update()
		{
			PlayerInputHandler();
			FaceMouse();
		}

		private void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(xVelocity * _speed, _rigidbody.velocity.y);
		}

		private void PlayerInputHandler() 
		{
			xVelocity = Input.GetAxis("Horizontal");

			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_direction = mousePosition - (Vector2)_weaponHolder.transform.position;

			if (Input.GetKeyDown(KeyCode.Space)) 
				Jump();
			
			if (Input.GetMouseButton(0)) 
				TryFire();
			
			if (Input.GetKeyDown(KeyCode.E)) 
				SetNextWeapon();

			if (Input.GetKeyDown(KeyCode.Q)) 
				SetPreviousWeapon();							
		}

		private void FaceMouse() 
		{
			_weaponHolder.transform.right = _direction;
		}		

		private void Jump() 
		{
			_rigidbody.AddForce(transform.up * _jumpForce);
		}		

		private void SetDefaultWeaponOnStart()
		{
			_weaponsCount = _weaponHolder.transform.childCount;
			_weapons = new GameObject[_weaponsCount];

			for (int i = 0; i < _weaponsCount; i++) 
			{
				_weapons[i] = _weaponHolder.transform.GetChild(i).gameObject;
				_weapons[i].SetActive(false);
			}

			_weapons[0].SetActive(true);
			_currentWeapon = _weapons[0];
			_currentWeaponIndex = 0;			
		}

		private void TryFire() 
		{
			var weapon = _weapons[_currentWeaponIndex].GetComponent<Weapon>();

			if (Time.time > _timeToShoot) 
			{
				_timeToShoot = Time.time + 1 / weapon.FireRate;
				weapon.Shoot();
			}
		}

		private void SetNextWeapon()
		{
			if (_currentWeaponIndex < _weaponsCount - 1) 
			{
				_weapons[_currentWeaponIndex].SetActive(false);
				_currentWeaponIndex ++;
				_weapons[_currentWeaponIndex].SetActive(true);
				_currentWeapon = _weapons[_currentWeaponIndex];
			}
		}

		private void SetPreviousWeapon() 
		{
			if (_currentWeaponIndex > 0) 
			{
				_weapons[_currentWeaponIndex].SetActive(false);
				_currentWeaponIndex --;
				_weapons[_currentWeaponIndex].SetActive(true);
				_currentWeapon = _weapons[_currentWeaponIndex];
			}
		}

		private void CreatePhysicsMaterial() 
		{
			_playerMaterial = new PhysicsMaterial2D();
			_playerMaterial.friction = 0.0f;
			_playerMaterial.bounciness = 0.0f;

			_rigidbody.sharedMaterial = _playerMaterial;
			_collider.sharedMaterial = _playerMaterial;
		}		
	}
}

