using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class CarPhysics : MonoBehaviour {

    [System.Serializable]
    public class OnImpactEvent : UnityEvent<float> {}
    public OnImpactEvent onImpact;

    [System.Serializable]
    public class OnImpactDetailedEvent : UnityEvent<float, Collision2D> {}
    public OnImpactDetailedEvent onImpactDetailed;

    private Transform cachedXf;

    private void Awake() {
        cachedXf = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var force = Mathf.Max(0.0f, -Vector2.Dot(cachedXf.up, collision.relativeVelocity));

        onImpact.Invoke(force);
        onImpactDetailed.Invoke(force, collision);
    }
}
