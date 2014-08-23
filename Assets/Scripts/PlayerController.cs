using UnityEngine;

[RequireComponent(typeof(Car))]
public class PlayerController : MonoSingleton<PlayerController> {

    private Car cachedCar;

    private void Awake() {
        cachedCar = GetComponent<Car>();
        cachedCar.directControl = Input.GetJoystickNames().Length != 0;
    }

    private void Update() {
        cachedCar.control = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
