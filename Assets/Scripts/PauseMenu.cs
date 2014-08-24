using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject window;

    private float timeScale;

    private void Awake() {
        timeScale = Time.timeScale;
    }

    private void Update() {
        if (Input.GetButtonDown("Pause")) {
            if (Time.timeScale == 0.0f) {
                Time.timeScale = timeScale;
                window.SetActive(false);
            }
            else {
                Time.timeScale = 0.0f;
                window.SetActive(true);
            }
        }

        if (Input.GetButtonDown("Accept") && Time.timeScale == 0.0f)
            LoadMainMenu();
    }

    public void LoadMainMenu() {
        Time.timeScale = timeScale;
        Application.LoadLevel(3);
    }

    public void ReturnToGame() {
        Time.timeScale = timeScale;
        window.SetActive(false);
    }
}
