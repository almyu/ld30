using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Rigidbody2D target;
    public float sensitivity = 0.1f;
    public float smoothness = 15.0f;

    private Transform cachedXf;
    private Vector3 fromTarget, desiredPosition;

    private void Awake() {
        cachedXf = transform;
        fromTarget = cachedXf.localPosition;
    }

    private void FixedUpdate() {
        desiredPosition = target.transform.position + fromTarget + (Vector3) target.velocity * sensitivity;
        cachedXf.position = Vector3.Lerp(cachedXf.position, desiredPosition, Time.fixedDeltaTime * smoothness);
    }
}
