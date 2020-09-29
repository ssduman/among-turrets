using UnityEngine;

public class Bullet : MonoBehaviour {

	public bool fromTurret = false;
	public float speed = 5f;
	public int damage = 50;
	public int playerDamage = 100;

	public Rigidbody2D rb;
	public GameObject impactEffect;

	private void OnTriggerEnter2D(Collider2D collision) {
		Turret turret = collision.GetComponent<Turret>();
		if (turret != null && !fromTurret) {
			turret.TakeDamage(damage);
			Destroy(gameObject);
			Instantiate(impactEffect, transform.position, transform.rotation);
		}

		PlayerMovement player = collision.GetComponent<PlayerMovement>();
		if (player != null && fromTurret) {
			player.TakeDamage(playerDamage);
		}

		if ((fromTurret && player != null || !fromTurret && turret != null) && player && player.health > 0) {
			Destroy(gameObject);
			Instantiate(impactEffect, transform.position, transform.rotation);
		}
	}

	private void OnBecameInvisible() {
		enabled = false;
		Destroy(gameObject);
	}
}
