using UnityEngine;
using System.Collections;

public class PlayerHP : MonoBehaviour {

    public SpriteRenderer pie;

    private Car cachedCar;
    private float max;

    private void Awake() {
        cachedCar = GetComponent<Car>();
        max = cachedCar.health;
    }

    private void Update() {
        pie.color = pie.color.ReplaceR(cachedCar.health / max);
    }
}
