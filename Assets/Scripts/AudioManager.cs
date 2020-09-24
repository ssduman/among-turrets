using UnityEngine;

public class AudioManager : MonoBehaviour {

	private static AudioSource audioSource;

	public static AudioClip run, hit, hitTurret, hurt, dead, explode, alarm, dialogue;

	private void Awake() {
		run = Resources.Load<AudioClip>("footstep_grass_001");
		hit = Resources.Load<AudioClip>("hit_013");
		hitTurret = Resources.Load<AudioClip>("hit_004");
		hurt = Resources.Load<AudioClip>("hurt_046");
		dead = Resources.Load<AudioClip>("hurt_117");
		explode = Resources.Load<AudioClip>("explosion_quick_18");
		alarm = Resources.Load<AudioClip>("Alarm_01");
		dialogue = Resources.Load<AudioClip>("PP_24");

		audioSource = gameObject.GetComponent<AudioSource>();
	}

	public static void Play(string audio) {
		if (audio == "run" && !audioSource.isPlaying) {
			audioSource.PlayOneShot(run);
		}

		if (audio == "hit") {
			audioSource.PlayOneShot(hit);
		}

		if (audio == "hitTurret") {
			audioSource.PlayOneShot(hitTurret);
		}

		if (audio == "hurt") {
			audioSource.PlayOneShot(hurt);
		}

		if (audio == "dead") {
			audioSource.PlayOneShot(dead);
		}

		if (audio == "explode") {
			audioSource.PlayOneShot(explode);
		}

		if (audio == "alarm") {
			audioSource.PlayOneShot(alarm);
		}

		if (audio == "dialogue") {
			audioSource.PlayOneShot(dialogue);
		}
	}

}
