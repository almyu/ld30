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

    private BaseEventData pointer;

    private bool pause = false;

    private void Awake() {
        pointer = new BaseEventData(system);
    }

    private void Update() {
        if (GameLogics.instance.isVictory && !pause) {
            ShowMenu(victoryText);
        }
        else if (GameLogics.instance.isDefeat && !pause) {
            ShowMenu(defeatText);
        }
        else if (Input.GetButtonDown("Pause")) {
            if (pause) {
                HideMenu();
            }
            else {
                ShowMenu(pauseText);
            }
        }
    }

    public void ShowMenu(string textMessage) {
        pause = true;
        text.text = textMessage;
        pointer.selectedObject = startSelected;
        system.SetSelectedGameObject(startSelected, pointer);
        Time.timeScale = 0.0f;
        window.SetActive(true);
    }

    public void HideMenu() {
        pause = false;
        Time.timeScale = 1.0f;
        window.SetActive(false);
        pointer.selectedObject = startSelected;
        system.SetSelectedGameObject(null, pointer);
    }

    public void YesButton() {
        Time.timeScale = 1.0f;
        Session.isNewGame = true;

        if (GameLogics.instance.isVictory || GameLogics.instance.isDefeat)
            Application.LoadLevel(Session.homeLevel + 1);
        else
            Application.LoadLevel(0);
    }

    public void NoButton() {
        Time.timeScale = 1.0f;
        if (GameLogics.instance.isVictory || GameLogics.instance.isDefeat)
            Application.LoadLevel(0);
        else
            window.SetActive(false);
    }
}
