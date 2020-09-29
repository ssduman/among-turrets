using UnityEngine;

public class AudioManager : MonoBehaviour {

	private static int foot = 0;
	private static AudioSource audioSource;

	public static AudioClip run, hit, hitTurret, hurt, dead, explode, alarm, dialogue;
	public static AudioClip[] footsteps;

	private void Awake() {
		run = Resources.Load<AudioClip>("footstep_grass_001");
		hit = Resources.Load<AudioClip>("hit_013");
		hitTurret = Resources.Load<AudioClip>("hit_004");
		hurt = Resources.Load<AudioClip>("hurt_046");
		dead = Resources.Load<AudioClip>("hurt_117");
		explode = Resources.Load<AudioClip>("explosion_quick_18");
		alarm = Resources.Load<AudioClip>("Alarm_01");
		dialogue = Resources.Load<AudioClip>("PP_24");

		footsteps = new AudioClip[5];
		footsteps[0] = Resources.Load<AudioClip>("footstep_1");
		footsteps[1] = Resources.Load<AudioClip>("footstep_2");
		footsteps[2] = Resources.Load<AudioClip>("footstep_3");
		footsteps[3] = Resources.Load<AudioClip>("footstep_4");
		footsteps[4] = Resources.Load<AudioClip>("footstep_5");

		audioSource = gameObject.GetComponent<AudioSource>();
	}

	public static void Play(string audio) {
		if (audio == "run" && !audioSource.isPlaying) {
			// audioSource.volume = Random.Range(0.7f, 1f);
			// audioSource.pitch = Random.Range(1.7f, 3f);
			if (foot >= 5) {
				foot = 0;
			}
			// int foot = Random.Range(0, 5);
			audioSource.PlayOneShot(footsteps[foot++]);
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
