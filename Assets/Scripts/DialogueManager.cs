using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	private Queue<string> sentences;

	public Canvas canvas;
	public Animator animator;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;

	void Start() {
		sentences = new Queue<string>();
		animator.SetBool("isOpen", false);
	}

	public void StartDialogue(Dialogue dialogue) {
		AudioManager.Play("dialogue");

		canvas.enabled = true;

		animator.enabled = true;
		animator.SetBool("isOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();
		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence() {
		if (sentences.Count == 0) {
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence) {
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	private void EndDialogue() {
		canvas.enabled = false;

		animator.SetBool("isOpen", false);
	}
}
