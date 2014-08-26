using UnityEngine;

public class ActionCharge : MonoBehaviour {

    public Car car;
    public Collider2D collider, trigger;
    public PlayerController controller;

    public float duration = 2.0f;
    public float power = 4.0f;

    private float initialShakePower;

    private void OnEnable() {
        collider.enabled = false;

        car.accelerationFactor = new Vector2(0.0f, power);

        controller.isBoost = true;

        initialShakePower = CameraFollow.instance.shakePower;
        CameraFollow.instance.shakePower = 0.0f;

        CameraFollow.instance.DetachTemporarily(duration);

        var trail = car.GetComponentInChildren<TrailRenderer>();
        if (trail != null)
            trail.enabled = true;

        trigger.enabled = true;
    }

    private void OnDisable() {
        collider.enabled = true;

        car.accelerationFactor = Vector2.one;

        controller.isBoost = false;

        CameraFollow.instance.shakePower = initialShakePower;

        var trail = car.GetComponentInChildren<TrailRenderer>();
        if (trail != null)
            trail.enabled = false;

        trigger.enabled = false;
    }
}
