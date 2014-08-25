using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject window;

    public EventSystem system;
    public GameObject startSelected;

    public string pauseText;
    public string victoryText;
    public string defeatText;

    public Text text;

    private float timeScale;

    private BaseEventData pointer;

    private void Awake() {
        timeScale = Time.timeScale;

        pointer = new BaseEventData(system);
    }

    private void Update() {
        if (GameLogics.instance.isVictory) {
            ShowMenu(victoryText);
        }
        else if (GameLogics.instance.isDefeat) {
            ShowMenu(defeatText);
        }
        else if (Input.GetButtonDown("Pause")) {
            if (Time.timeScale == 0.0f) {
                HideMenu();
            }
            else {
                ShowMenu(pauseText);
            }
        }
    }

    public void ShowMenu(string textMessage) {
        pointer.selectedObject = startSelected;
        system.SetSelectedGameObject(startSelected, pointer);
        text.text = pauseText;
        Time.timeScale = 0.0f;
        window.SetActive(true);
    }

    public void HideMenu() {
        Time.timeScale = timeScale;
        window.SetActive(false);
        pointer.selectedObject = startSelected;
        system.SetSelectedGameObject(null, pointer);
    }

    public void YesButton() {
        Time.timeScale = timeScale;
        if (GameLogics.instance.isVictory || GameLogics.instance.isDefeat) {
            PlayerPrefs.SetInt("NewGame", 1);
            Application.LoadLevel(GameLogics.instance.homeLevel);
        }
        Application.LoadLevel(3);
    }

    public void NoButton() {
        Time.timeScale = timeScale;
        if (GameLogics.instance.isVictory || GameLogics.instance.isDefeat)
            Application.LoadLevel(3);
        window.SetActive(false);
    }
}
