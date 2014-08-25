using UnityEngine;
using System.Collections;

public class HealthIndicator : MonoBehaviour {

    public bool isPlayer = false;

    public float healSpeed = 0.0f;

    public SpriteRenderer pie;

    private Car cachedCar;
    private float max;

    private void Awake() {
        cachedCar = GetComponent<Car>();
        max = isPlayer ? GameLogics.instance.maxPlayerHealth : cachedCar.health;

        if (isPlayer)
            cachedCar.health = Session.health;
    }

    private void Update() {
        pie.color = pie.color.ReplaceR(cachedCar.health / max);

        if (LevelSettings.instance.levelIndex == Session.homeLevel)
            cachedCar.health = (cachedCar.health + healSpeed) < max ? cachedCar.health + healSpeed : max;

        if (isPlayer)
            Session.health = cachedCar.health;
    }
}
