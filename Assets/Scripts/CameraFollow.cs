using UnityEngine;

public class CameraFollow : MonoSingleton<CameraFollow> {

    public Rigidbody2D target;
    public float sensitivity = 0.1f;
    public float smoothness = 15.0f;
    public float shakeExponent = 3.0f;
    public float shakePower = 1.0f;

    private Transform cachedXf;
    private Vector3 fromTarget;
    private float detachTimer;

    private void Awake() {
        cachedXf = transform;
        fromTarget = cachedXf.localPosition;

        target = GameObject.Find("Player").rigidbody2D;
    }

    private void FixedUpdate() {
        detachTimer = Mathf.Max(0.0f, detachTimer - Time.fixedDeltaTime);
        var catchup = Mathf.Clamp01(Mathf.Pow(detachTimer, shakeExponent));

        var desiredOffset = (Vector3) target.velocity * sensitivity * (1.0f - catchup);
        desiredOffset = desiredOffset.normalized * Mathf.Min(camera.orthographicSize, desiredOffset.magnitude);

        var desiredPosition = target.transform.position + fromTarget + desiredOffset;
        cachedXf.position = Vector3.Lerp(cachedXf.position, desiredPosition, Time.fixedDeltaTime * smoothness) + Random.insideUnitSphere * shakePower * catchup;
    }

    public void DetachTemporarily(float duration) {
        detachTimer = duration;
    }
}
