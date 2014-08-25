using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Car : MonoBehaviour {

    public Transform inner;
    public bool directControl = true;
    public float acceleration = 20.0f;
    public float turningSpeed = 25.0f;

    public float health = 100.0f;

    [System.Serializable]
    public class OnDriveEvent : UnityEvent<bool> {}
    public OnDriveEvent onDrive;

    [System.Serializable]
    public class OnSteerEvent : UnityEvent<float> {}
    public OnSteerEvent onSteer;

    public UnityEvent onDeath;

    [HideInInspector]
    public Vector2 control {
        get { return _control; }
        set {
            bool wasStill = Mathf.Approximately(_control.y, 0.0f);
            bool nowStill = Mathf.Approximately(value.y, 0.0f);

            if (wasStill != nowStill)
                onDrive.Invoke(wasStill);

            if (!Mathf.Approximately(_control.x, value.x))
                onSteer.Invoke(value.x);

            _control = value;
        }
    }

    private Vector2 _control;
    private Rigidbody2D cachedBody;

    private void Awake() {
        cachedBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (directControl) {
            cachedBody.AddForce(control * Time.fixedDeltaTime * acceleration, ForceMode2D.Impulse);

            var vel = cachedBody.velocity;
            if (vel.sqrMagnitude >= Mathf.Epsilon)
                inner.up = Vector2.Lerp(inner.up, vel.normalized, Time.fixedDeltaTime * turningSpeed * 1.5f);
        }
        else {
            inner.Rotate(Vector3.back, control.x * Time.fixedDeltaTime * Vector3.Dot(cachedBody.velocity, inner.up) * turningSpeed);
            cachedBody.AddForce(inner.up * Time.fixedDeltaTime * control.y * acceleration, ForceMode2D.Impulse);
        }
    }

    public void Hit(float force) {
        if (health <= Mathf.Epsilon) return;

        health -= force;

        if (health <= Mathf.Epsilon)
            onDeath.Invoke();
    }

    public void Kill() {
        Destroy(gameObject);
    }
}
