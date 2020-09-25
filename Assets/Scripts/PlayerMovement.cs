using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private Vector2 movement;
	private bool mobile = false;
	private bool isAiming = false;
	private bool joystickSelection = false;

	public int health = 100;
	public bool isGrounded = false;
	public float speed = 5f;
	public float jump_force = 5f;
	public bool facingRight = true;
	public float horizontalMove;
	public float verticalMove;

	public Transform firePoint;
	public Animator animator;
	public Rigidbody2D rg;
	public Transform player;
	public SpriteRenderer srenderer;
	public Canvas joystickCanvas;
	public Joystick joystick;

	private void Awake() {
		joystickSelection = PlayerPrefs.GetInt("UseJoystick") == 1 ? true : false;
		if (SystemInfo.deviceType == DeviceType.Handheld || Application.isMobilePlatform || joystickSelection) {
			mobile = true;
			joystickCanvas.enabled = true;
			joystickCanvas.gameObject.SetActive(true);
		}
		verticalMove = -1f;
	}

	private void Update() {
		if (health <= 0) {
			return;
		}
		if (joystickCanvas.gameObject.activeSelf || mobile) {
			horizontalMove = joystick.Horizontal;
			verticalMove = joystick.Vertical;
		}
		else {
			horizontalMove = Input.GetAxisRaw("Horizontal");
			verticalMove = Input.GetAxisRaw("Vertical");
		}
		movement = new Vector3(horizontalMove, verticalMove);

		float Speed = Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y));
		if (movement != Vector2.zero) {
			animator.SetFloat("Horizontal", horizontalMove);
			animator.SetFloat("Vertical", verticalMove);
		}
		animator.SetFloat("Speed", Speed);

		NewFirePoint();

		if (movement.x > 0 && !facingRight) {
			transform.Rotate(0f, 180f, 0f);
			facingRight = true;
		}
		if (movement.x < 0 && facingRight) {
			transform.Rotate(0f, 180f, 0f);
			facingRight = false;
		}

		if (Input.GetMouseButtonDown(1) && !isAiming) {
			animator.SetBool("Aim", true);
			isAiming = true;
			if (speed == 5f) {
				speed /= 2;
			}
		}
		else if (Input.GetMouseButtonDown(1) && isAiming) {
			animator.SetBool("Aim", false);
			isAiming = false;
			if (speed == 2.5f) {
				speed *= 2;
			}
		}
	}

	private void FixedUpdate() {
		if (health > 0) {
			// transform.position += new Vector3(movement.x, movement.y, 0f) * Time.deltaTime * speed;
			rg.MovePosition(rg.position + movement * speed * Time.fixedDeltaTime);

			if (movement != Vector2.zero) {
				AudioManager.Play("run");
			}
		}
	}

	private void NewFirePoint() {
		if (animator.GetFloat("Vertical") > 0) {
			firePoint.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
		if (animator.GetFloat("Vertical") < 0) {
			firePoint.transform.localPosition = new Vector3(0.04f, 0.04f, 0f);
		}
		if (animator.GetFloat("Horizontal") != 0) {
			firePoint.transform.localPosition = new Vector3(0.2f, 0.06f, 0f);
		}
	}

	private void Jump() {
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
			animator.SetBool("Jump", true);
			rg.AddForce(new Vector2(0f, jump_force), ForceMode2D.Impulse);
		}

		if (rg.velocity.y < -0.01) {
			animator.SetBool("Jump", false);
			animator.SetBool("Fall", true);
		}

		if (rg.velocity.y == 0) {
			animator.SetBool("Fall", false);
		}
	}

	public void TakeDamage(int damage) {
		health -= damage;
		if (health <= 0f) {
			animator.SetBool("Dead", true);
			AudioManager.Play("dead");
		}
		else {
			AudioManager.Play("hurt");
		}
	}
}
