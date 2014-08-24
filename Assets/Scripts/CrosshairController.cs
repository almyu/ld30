using UnityEngine;

public class CrosshairController : MonoBehaviour {

    public bool joystickLook = false;
    public bool joystickTurn = true;
    public float joystickTurnSpeed = 180.0f;

    private Transform cachedXf;

    private void Awake() {
        cachedXf = transform;
    }

    private void Update() {
        if (joystickLook || !Input.mousePresent) {
            var look = new Vector2(Input.GetAxis("LookHorizontal"), Input.GetAxis("LookVertical"));
            if (look.sqrMagnitude > Mathf.Epsilon)
                if (joystickTurn)
                    cachedXf.Rotate(Vector3.back, -look.x * joystickTurnSpeed * Time.deltaTime);
                else
                    cachedXf.up = look;
        }
        else if (Input.mousePresent) {
            var point = (Vector3)(Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cachedXf.up = (point - cachedXf.position).normalized;
        }
    }
}
