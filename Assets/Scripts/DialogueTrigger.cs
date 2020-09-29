using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public Transform player;

	public void TriggerDialogue() {
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("PlayerFoot")) {
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
			PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
			playerScript.speed = 0f;
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.collider.CompareTag("PlayerFoot")) {
		}
	}
}
