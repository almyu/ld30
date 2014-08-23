using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Rigidbody2D target;
    public float sensitivity = 0.1f;

    private Transform cachedXf;
    private Vector3 initialPosition;

    private void Awake() {
        cachedXf = transform;
        initialPosition = cachedXf.localPosition;
    }

    private void FixedUpdate() {
        cachedXf.localPosition = initialPosition + (Vector3) target.velocity * sensitivity;
    }
}
