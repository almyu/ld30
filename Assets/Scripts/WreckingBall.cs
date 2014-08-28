using UnityEngine;
using UnityEngine.Events;

public class WreckingBall : MonoBehaviour {

    public LayerMask layers = -1;
    public float damage = 20.0f;
    public UnityEvent onImpact;

    private void OnCollisionEnter2D(Collision2D coll) {
        if (((1 << coll.gameObject.layer) & layers.value) == 0) return;

        var car = coll.gameObject.GetComponent<Car>();
        if (car == null) return;

        car.Hit(coll.relativeVelocity.magnitude * damage);
        onImpact.Invoke();

        CameraFollow.instance.DetachTemporarily(1.0f);
    }
}
