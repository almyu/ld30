using UnityEngine;
using System.Collections;

public class GameLogics : MonoSingleton<GameLogics> {

    public float increasingRate = 4.0f;

    public bool isVictory = false;
    public bool isDefeat = false;

    public float victoryFactor = 0.8f;
    public float defeatFactor = 0.1f;
    public int homeLevel;

    public float maxPlayerHealth = 80.0f;

    private void Awake() {
        homeLevel = Session.homeLevel;

        if (Session.isNewGame) {
            ResetState();
            Session.isNewGame = false;
        }

        var player = GameObject.Find("Player").GetComponent<Car>();
        player.onDeath.AddListener(() => Defeat());
    }

    private void Update() {
        Session.stats = new Vector3(homeLevel != 0 ? Session.stats.x + Time.deltaTime * increasingRate : Session.stats.x,
                            homeLevel != 1 ? Session.stats.y + Time.deltaTime * increasingRate : Session.stats.y,
                            homeLevel != 2 ? Session.stats.z + Time.deltaTime * increasingRate : Session.stats.z);

        if (stateHome < defeatFactor)
            isDefeat = true;
        if (stateHome > victoryFactor)
            isVictory = true;
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
            var s = Session.stats.x + Session.stats.y + Session.stats.z;
        
            return new Vector3(Session.stats.x / s, Session.stats.y / s, Session.stats.z / s);
        }
    }

    public void ResetState() {
        Session.stats = Vector3.one * 100.0f;
        Session.health = maxPlayerHealth;
    }

    private void Defeat() {
        isDefeat = true;
    }

    private void Victory() {
        isVictory = true;
    }
}
