using UnityEngine;
using System.Collections;

public class GameLogics : MonoBehaviour {

    public static GameLogics instance;

    public Vector3 stats;

    public float increasingRate = 4.0f;

    public bool isVictory = false;
    public bool isDefeat = false;

    public float victoryFactor = 0.8f;
    public float defeatFactor = 0.1f;
    public int homeLevel;

    private void Awake() {
        instance = this;

        homeLevel = PlayerPrefs.GetInt("HomeLevel", 0);

        if (PlayerPrefs.GetInt("NewGame", 1) == 1) {
            ResetState();
        }

        stats = new Vector3(PlayerPrefs.GetFloat("RustPoints", 100.0f),
                            PlayerPrefs.GetFloat("NeonPoints", 100.0f),
                            PlayerPrefs.GetFloat("WoodenPoints", 100.0f));
    }

    private void Update() {
        stats = new Vector3(homeLevel != 0 ? stats.x + Time.deltaTime * increasingRate : stats.x,
                            homeLevel != 1 ? stats.y + Time.deltaTime * increasingRate : stats.y,
                            homeLevel != 2 ? stats.z + Time.deltaTime * increasingRate : stats.z);

        if (stateHome < defeatFactor)
            isDefeat = true;
        if (stateHome > victoryFactor)
            isVictory = true;
    }

    private void OnDestroy() {
        PlayerPrefs.SetFloat("RustPoints", stats.x);
        PlayerPrefs.SetFloat("NeonPoints", stats.y);
        PlayerPrefs.SetFloat("WoodenPoints", stats.z);
    }

    public float stateHome {
        get {
            var state = 0.0f;
            switch (homeLevel) {
                case 0: state = statsNormalized.x; break;
                case 1: state = statsNormalized.y; break;
                case 2: state = statsNormalized.z; break;
            }
            return state;
        }
    }

    public Vector3 statsNormalized {
        get {
            var s = stats.x + stats.y + stats.z;
        
            return new Vector3(stats.x / s, stats.y / s, stats.z / s);
        }
    }

    public void ResetState() {
        PlayerPrefs.SetFloat("RustPoints", 100.0f);
        PlayerPrefs.SetFloat("NeonPoints", 100.0f);
        PlayerPrefs.SetFloat("WoodenPoints", 100.0f);
    }
}
