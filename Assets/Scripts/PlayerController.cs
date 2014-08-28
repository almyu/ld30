using UnityEngine;

[RequireComponent(typeof(Car))]
public class PlayerController : MonoSingleton<PlayerController> {

    public float controlLockFactor = 0.0f;

    private Car cachedCar;
    private Vector2 lockedControl;

    private void Awake() {
        cachedCar = GetComponent<Car>();
        cachedCar.directControl = PlayerPrefs.GetInt("DirectControl", 0) != 0;

        cachedCar.control = Session.vectors.Get("LastControl", Vector3.zero);
    }

    private void OnDestroy() {
        Session.vectors.Set("LastControl", cachedCar.control);
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
