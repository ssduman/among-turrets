using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {

	public bool UseJoystick = false;
	public TextMeshProUGUI JoystickText;

	private void Awake() {
		PlayerPrefs.SetInt("UseJoystick", 0);
	}

	public void PlayGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void ToggleJoystick() {
		UseJoystick = !UseJoystick;
		int pref = UseJoystick == true ? 1 : 0;
		PlayerPrefs.SetInt("UseJoystick", pref);
		if (!UseJoystick) {
			JoystickText.text = "Use Joystick";
		}
		else {
			JoystickText.text = "Use Keyboard";
		}
	}
}