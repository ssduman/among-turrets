using UnityEngine;

public class TurretTop : MonoBehaviour {

	PlayerMovement playerHealth;
	private bool playerFound = false;

	public float maxDistance = 8f;
	public float rotatinSpeed = 5f;
	public float bulletSpeed = 5f;
	public Transform player;
	public SpriteRenderer srenderer;
	public Transform firePoint;
	public GameObject bulletPrefab;

	private void Awake() {
		srenderer = gameObject.GetComponent<SpriteRenderer>();
		playerHealth = player.transform.GetComponent<PlayerMovement>();
	}

	private void Update() {
		if (playerHealth.health <= 0) {
			CancelInvoke("FireBullet");
			return;
		}
		float distance = Vector2.Distance(player.transform.position, transform.position);

		if (distance <= maxDistance) {
			RotateGun();
		}
		if (distance <= maxDistance && !playerFound) {
			InvokeRepeating("FireBullet", 0.5f, 0.5f);
			playerFound = true;

			AudioManager.Play("alarm");
		}
		if (distance > maxDistance && playerFound) {
			CancelInvoke("FireBullet");
			playerFound = false;
		}
	}

	private void FireBullet() {
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet t_bullet = bullet.GetComponent<Bullet>();
		t_bullet.fromTurret = true;

		SpriteRenderer sr_bullet = bullet.GetComponent<SpriteRenderer>();
		sr_bullet.color = Color.red;
		Rigidbody2D rb_bullet = bullet.GetComponent<Rigidbody2D>();

		Vector3 bulletDirection = player.transform.position - firePoint.transform.position;
		bulletDirection.z = 0f;

		rb_bullet.velocity = bulletDirection.normalized * bulletSpeed;
	}

	private void RotateGun() {
		Vector2 direction = player.transform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotatinSpeed);

		if (Mathf.Abs(angle) <= 90f) {
			srenderer.flipY = false;
		}
		else {
			srenderer.flipY = true;
		}
	}
}
