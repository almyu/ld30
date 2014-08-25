using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class CarPhysics : MonoBehaviour {

    public Transform innerXf;

    public LayerMask damagingLayers = -1;
    public float deadZone = 90.0f;

    [System.Serializable]
    public class OnImpactEvent : UnityEvent<float> {}
    public OnImpactEvent onImpact;

    [System.Serializable]
    public class OnImpactDetailedEvent : UnityEvent<float, Collision2D> {}
    public OnImpactDetailedEvent onImpactDetailed;

    private void Awake() {
        if (innerXf == null)
            innerXf = transform.Find("Inner").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var mask = 1 << collision.gameObject.layer;
        if ((damagingLayers.value & mask) == 0) return;
        if (Vector2.Angle(innerXf.up, collision.relativeVelocity) < deadZone * 0.5f) return;

        var force = Mathf.Max(0.0f, -Vector2.Dot(innerXf.up, collision.relativeVelocity));

        onImpact.Invoke(force);
        onImpactDetailed.Invoke(force, collision);
    }
}
