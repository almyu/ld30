using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EndMenu : MonoBehaviour {

    public GameObject window;

    public EventSystem system;
    public GameObject startSelected;

    public string endText;

    public Color victoryColor;
    public Color defeatColor;

    public Text text;

    private BaseEventData pointer;

    private bool pause = false;

    private void Awake() {
        pointer = new BaseEventData(system);
    }

    private void Update() {
        if (GameLogics.instance.isVictory && !pause) {
            var hexColor = victoryColor.ReplaceA(1.0f).ToHexString();
            ShowMenu("<color=#" + hexColor + ">Victory\n</color>" + endText);
        }
        else if (GameLogics.instance.isDefeat && !pause) {
            var hexColor = defeatColor.ReplaceA(1.0f).ToHexString();
            ShowMenu("<color=#" + hexColor + ">Defeat\n</color>" + endText);
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

    public void YesButton() {
        Time.timeScale = 1.0f;
        Session.isNewGame = true;
        Application.LoadLevel(0);
    }
}
