using UnityEngine;
using System.Collections;

public class WorldsStats : MonoBehaviour {

    public static WorldsStats instance;

    public Vector3 stats;

    public float increasingRate = 4.0f;

    private int homeLevel;

    private void Awake() {
        instance = this;

        homeLevel = PlayerPrefs.GetInt("HomeLevel", 0);

        stats = new Vector3(PlayerPrefs.GetFloat("RustPoints", 100.0f),
                            PlayerPrefs.GetFloat("NeonPoints", 100.0f),
                            PlayerPrefs.GetFloat("WoodenPoints", 100.0f));
    }

    private void Update() {
        stats = new Vector3(homeLevel != 0 ? stats.x + Time.deltaTime * increasingRate : stats.x,
                            homeLevel != 1 ? stats.y + Time.deltaTime * increasingRate : stats.y,
                            homeLevel != 2 ? stats.z + Time.deltaTime * increasingRate : stats.z);
    }

    private void OnDestroy() {
        PlayerPrefs.SetFloat("RustPoints", stats.x);
        PlayerPrefs.SetFloat("NeonPoints", stats.y);
        PlayerPrefs.SetFloat("WoodenPoints", stats.z);
    }

    public Vector3 statsNormalized {
        get {
            var s = stats.x + stats.y + stats.z;
        
            return new Vector3(stats.x / s, stats.y / s, stats.z / s);
        }
    }
}
