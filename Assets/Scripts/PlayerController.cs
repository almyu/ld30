using UnityEngine;

[RequireComponent(typeof(Car))]
public class PlayerController : MonoBehaviour {

    private Car cachedCar;

    private void Awake() {
        cachedCar = GetComponent<Car>();
    }

    private void Update() {
        cachedCar.control = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }
}
