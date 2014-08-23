using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Car : MonoBehaviour {

    public Transform inner;
    public float acceleration = 20.0f;

    [HideInInspector]
    public Vector2 control;

    private Rigidbody2D cachedBody;

    private void Awake() {
        cachedBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        inner.Rotate(Vector3.back, control.x * Time.fixedDeltaTime * Vector3.Dot(cachedBody.velocity, inner.up) * acceleration / 2.0f);
        cachedBody.AddForce(inner.up * Time.fixedDeltaTime * control.y * acceleration, ForceMode2D.Impulse);
    }
}
