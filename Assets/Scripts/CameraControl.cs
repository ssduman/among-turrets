using UnityEngine;

public class CameraControl : MonoBehaviour {

	private Vector3 offset = new Vector3(0f, 0f, -10f);

	public Transform player;

	void Update() {
		transform.position = player.position + offset;
	}
}
