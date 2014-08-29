using UnityEngine;
using System.Collections;

public class TripleBar : MonoBehaviour {

    public RectTransform rust, neon, wooden;
    public RectTransform rustToNeon, neonToWooden;

    private void Update() {
        var stats = GameLogics.instance.statsNormalized;
        rust.anchorMin = new Vector3(0.0f, 0.0f);
        rust.anchorMax = new Vector2(stats.x, 1.0f);
        neon.anchorMin = new Vector2(stats.x, 0.0f);
        neon.anchorMax = new Vector2(stats.x + stats.y, 1.0f);
        wooden.anchorMin = new Vector2(stats.x + stats.y, 0.0f);
        wooden.anchorMax = new Vector2(1.0f, 1.0f);

        rustToNeon.anchorMin = rustToNeon.anchorMax = new Vector2(stats.x, 0.5f);
        neonToWooden.anchorMin = neonToWooden.anchorMax = new Vector2(stats.x + stats.y, 0.5f);
    }
}
