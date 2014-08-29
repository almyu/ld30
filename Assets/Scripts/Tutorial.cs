using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public static string[] factionColorNames = new[] { "Red", "Blue", "Green" };

    public Graphic[] colorRefs;
    public float fadeoutStart = 5.0f, fadeoutTime = 5.0f;

    private void Awake() {
        if (Session.bools.Get("TutorialShown")) {
            gameObject.SetActive(false);
            return;
        }

        Session.bools.Set("TutorialShown", true);

        var hexColor = colorRefs[Session.homeLevel].color.ReplaceA(1.0f).ToHexString();
        var factionText = "<color=#" + hexColor + ">" + factionColorNames[Session.homeLevel] + "</color>";

        foreach (var text in GetComponentsInChildren<Text>()) {
            text.text = text.text.Replace("*", factionText);
        }
    }

    private void Update() {
        if (Time.timeSinceLevelLoad < fadeoutStart) return;
        if (Time.timeSinceLevelLoad > fadeoutStart + fadeoutTime) {
            gameObject.SetActive(false);
            return;
        }

        var alpha = (fadeoutStart + fadeoutTime - Time.timeSinceLevelLoad) / fadeoutTime;

        foreach (var text in GetComponentsInChildren<Text>())
            text.color = text.color.ReplaceA(alpha);
    }
}
