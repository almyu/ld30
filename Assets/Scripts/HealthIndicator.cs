using UnityEngine;
using System.Collections;

public class HealthIndicator : MonoBehaviour {

    public SpriteRenderer pie;

    private Car cachedCar;
    private float max; //TODO 

    private void Awake() {
        cachedCar = GetComponent<Car>();
        max = cachedCar.health;
    }

    private void Update() {
        pie.color = pie.color.ReplaceR(cachedCar.health / max);
    }
}
