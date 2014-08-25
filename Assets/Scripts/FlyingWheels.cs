using UnityEngine;

public class FlyingWheels : MonoBehaviour {

    public GameObject prefab;
    public float radialOffset = 1.0f;
    public float impulse = 50.0f, torque = 1.0f;

    public void Awake() {
        var pos = transform.position;

        for (int i = 0; i < 4; ++i) {
            var wheel = (GameObject) Instantiate(prefab, pos + (Vector3) Random.insideUnitCircle.normalized , Quaternion.Euler(Vector3.forward * Random.value * 360.0f));
            var body = wheel.rigidbody2D;

            body.AddTorque(Random.Range(-1.0f, 1.0f) * body.mass * torque, ForceMode2D.Impulse);
            body.AddForce(Random.insideUnitCircle * body.mass * impulse, ForceMode2D.Impulse);
        }
    }
}
