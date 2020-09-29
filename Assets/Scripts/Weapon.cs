using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

	private Vector3 previous = Vector3.zero;
	private Vector3 velocity;

	public float bulletSpeed = 6f;

	public PlayerMovement player;
	public Transform firePoint;
	public GameObject bulletPrefab;
	public Rigidbody2D rb_player;
	public Animator animator;
	public Button shootButton;

	private void Awake() {
		rb_player = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
		shootButton.onClick.AddListener(Shoot);
	}

	private void Update() {
		velocity = ((transform.position - previous) / Time.deltaTime).normalized;
		previous = transform.position;

		if (Input.GetKeyDown("space")) { 
			Shoot();
		}
	}

	private void Shoot() {
		if (animator.GetBool("Aim") == false) {
			CancelInvoke("AimFalse");
			animator.SetBool("Aim", true);
			//if (player.speed == 5f) {
			//	player.speed /= 2;
			//}
			Invoke("AimFalse", 2f);
		}

		AudioManager.Play("hit");

		float horizontalMove = animator.GetFloat("Horizontal");
		float verticalMove = animator.GetFloat("Vertical");

		bool Left = horizontalMove < 0 ? true : false;
		bool Right = horizontalMove > 0 ? true : false;
		bool Front = verticalMove < 0 ? true : false;
		bool Back = verticalMove > 0 ? true : false;

		Vector2 bulletDirection = Vector3.zero;
		if (Left) {
			bulletDirection = Vector3.left;
		}
		if (Right) {
			bulletDirection = Vector3.right;
		}
		if (Front) {
			bulletDirection = Vector3.down;
		}
		if (Back) {
			bulletDirection = Vector3.up;
		}

		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D rb_bullet = bullet.GetComponent<Rigidbody2D>();

		Vector2 bulletVelocity = Vector2.zero;
		bulletVelocity.x = velocity.x + bulletDirection.x;
		bulletVelocity.y = velocity.y + bulletDirection.y;

		rb_bullet.velocity = bulletVelocity * bulletSpeed;
	}

	private void AimFalse() {
		//if (player.speed == 2.5f) {
		//	player.speed *= 2;
		//}
		animator.SetBool("Aim", false);
	}
}
