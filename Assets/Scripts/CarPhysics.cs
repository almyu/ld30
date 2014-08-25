using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class CarPhysics : MonoBehaviour {

    [System.Serializable]
    public class OnImpactEvent : UnityEvent<float> {}
    public OnImpactEvent onImpact;

    private Transform cachedXf;

    private void Awake() {
        cachedXf = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        onImpact.Invoke(Mathf.Max(0.0f, -Vector2.Dot(cachedXf.up, collision.relativeVelocity)));
    }
}
