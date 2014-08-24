using UnityEngine;
using System.Collections;

public class TripleBar : MonoBehaviour {

    public RectTransform rust;
    public RectTransform neon;
    public RectTransform wooden;

    private void Update() {
        var stats = WorldsStats.instance.statsNormalized;
        rust.anchorMin = new Vector3(0.0f, 0,0f);
        rust.anchorMax = new Vector3(stats.x, 1,0f);
        neon.anchorMin = new Vector3(stats.x, 0,0f);
        neon.anchorMax = new Vector3(stats.x + stats.y, 1,0f);
        wooden.anchorMin = new Vector3(stats.x + stats.y, 0,0f);
        wooden.anchorMax = new Vector3(1.0f, 1,0f);
    }
}
