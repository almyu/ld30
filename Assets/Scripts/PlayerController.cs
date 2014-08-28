using UnityEngine;

[RequireComponent(typeof(Car))]
public class PlayerController : MonoSingleton<PlayerController> {

    public float controlLockFactor = 0.0f;

    private Car cachedCar;
    private Vector2 lockedControl;

    private void Awake() {
        cachedCar = GetComponent<Car>();
        cachedCar.directControl = PlayerPrefs.GetInt("DirectControl", 0) != 0;

        cachedCar.control = Session.vectors.Get("LastControl");
        cachedCar.rigidbody2D.velocity = Session.vectors.Get("LastVelocity");
        cachedCar.inner.transform.up = Session.vectors.Get("LastDirection", Vector2.up);
    }

    private void OnDestroy() {
        Session.vectors.Set("LastControl", cachedCar.control);
        Session.vectors.Set("LastVelocity", cachedCar.rigidbody2D.velocity);
        Session.vectors.Set("LastDirection", cachedCar.inner.transform.up);
    }

    private Vector2 GetAxes() {
        var axes = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (cachedCar.directControl) {
            if (axes.sqrMagnitude > 1.0f)
                axes.Normalize();
        }
        else
            axes.y *= Mathf.Ceil(Mathf.Abs(axes.y));

        return axes;
    }

    private void Update() {
        cachedCar.control = Vector2.Lerp(GetAxes(), lockedControl, controlLockFactor);
    }

    public void LockControl() {
        if (cachedCar.directControl)
            lockedControl = GetAxes();
        else
            lockedControl = Vector2.up;
    }
}
