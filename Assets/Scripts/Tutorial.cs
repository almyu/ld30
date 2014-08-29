using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public static string[] factionColorNames = new[] { "Red", "Blue", "Green" };

    public float fadeoutStart = 5.0f, fadeoutTime = 5.0f;

    private void Awake() {
        foreach (var text in GetComponentsInChildren<Text>()) {
            text.text = text.text.Replace("*", factionColorNames[Session.homeLevel]);
        }
    }

    private void Update() {
        if (Time.timeSinceLevelLoad < fadeoutStart) return;
        if (Time.timeSinceLevelLoad > fadeoutStart + fadeoutTime) {
            enabled = false;
            return;
        }

        var alpha = (fadeoutStart + fadeoutTime - Time.timeSinceLevelLoad) / fadeoutTime;

        foreach (var text in GetComponentsInChildren<Text>())
            text.color = text.color.ReplaceA(alpha);
    }
}
