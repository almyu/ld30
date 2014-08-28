using UnityEngine;

public class ActionCharge : MonoBehaviour {

    public Car car;
    public Collider2D mainCollider, trigger;
    public PlayerController controller;

    public float duration = 2.0f;
    public float power = 4.0f;

    private float initialShakePower;

    private void OnEnable() {
        mainCollider.enabled = false;

        car.accelerationFactor = Vector2.one * power;

        controller.LockControl();

        initialShakePower = CameraFollow.instance.shakePower;
        CameraFollow.instance.shakePower = 0.0f;

        CameraFollow.instance.DetachTemporarily(duration);

        var trail = car.GetComponentInChildren<TrailRenderer>();
        if (trail != null)
            trail.enabled = true;

        trigger.enabled = true;
    }

    private void LateUpdate() {
        // Revert animator's changes
        controller.controlLockFactor = 1.0f;
    }

    private void OnDisable() {
        mainCollider.enabled = true;

        car.accelerationFactor = Vector2.one;

        controller.controlLockFactor = 0.0f;

        CameraFollow.instance.shakePower = initialShakePower;

        var trail = car.GetComponentInChildren<TrailRenderer>();
        if (trail != null)
            trail.enabled = false;

        trigger.enabled = false;
    }
}
