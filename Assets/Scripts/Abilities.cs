using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Abilities : MonoBehaviour {
	public Color inactiveColor, activeColor;

	public Image[] imageActions = new Image[4];
	public Text[] textActions = new Text[4];

	public Action[] actions = new Action[4];

    private void Awake() {
        actions = FindObjectsOfType<Action>();
    }

    private void Update() {
    	for (int i = 0; i < actions.Length; ++i) {
    		if(actions[i].cooldownTimer != 0.0f) {
				imageActions[i].color = inactiveColor;
				textActions[i].text = actions[i].cooldown.ToString();
    		}
    		else {
    			imageActions[i].color = activeColor;
				textActions[i].text = "";
    		}
    	}
    }
}
