using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Rigidbody2D target;
    public float sensitivity = 0.1f;
    public float smoothness = 15.0f;

    private Transform cachedXf;
    private Vector3 initialPosition, offset;

    private void Awake() {
        cachedXf = transform;
        initialPosition = cachedXf.localPosition;
    }

    private void FixedUpdate() {
        offset = Vector3.Lerp(offset, target.velocity * sensitivity, smoothness);
        cachedXf.localPosition = initialPosition + offset;
    }
}
