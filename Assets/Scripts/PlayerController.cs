using UnityEngine;

[RequireComponent(typeof(Car))]
public class PlayerController : MonoBehaviour {

    public static PlayerController instance;
    
    private Car cachedCar;

    private void Awake() {
        instance = this;
        
        cachedCar = GetComponent<Car>();
    }

    private void Update() {
        cachedCar.control = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
