using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Abilities : MonoBehaviour {
    public Color inactiveColor, activeColor;

    public Image[] imageActions = new Image[4];
    public Text[] textActions = new Text[4];

    public Action[] actions = new Action[4];

    private void Awake() {
        var names = new[] { "Charge", "Chainball", "Barrier", "Ultimate" };

        foreach (var action in FindObjectsOfType<Action>()) {
            for (int i = 0; i < 4; ++i) {
                if (action.name == names[i]) {
                    actions[i] = action;
                    break;
                }
            }
        }
    }

    private void Update() {
        for (int i = 0; i < actions.Length; ++i) {
            if(actions[i].cooldownTimer != 0.0f) {
                imageActions[i].color = inactiveColor;
                textActions[i].text = Mathf.CeilToInt(actions[i].cooldownTimer).ToString();
            }
            else {
                imageActions[i].color = activeColor;
                textActions[i].text = "";
            }
        }
    }
}
