using UnityEngine;
using UnityEngine.Events;

public enum Action {
    Charge,
    FrontBarrier,
    BackBarrier,
    Ultimate
}

public class ActionInput : MonoBehaviour {

    public UnityEvent onPin, onUnpin;
    public UnityEvent onAction0, onAction1, onAction2, onAction3;

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            onPin.Invoke();

        if (Input.GetButtonDown("Jump")) {
            onAction0.Invoke();
            onUnpin.Invoke();
        }
        else if (Input.GetButtonDown("Fire1")) {
            onAction1.Invoke();
            onUnpin.Invoke();
        }
        else if (Input.GetButtonDown("Fire2")) {
            onAction2.Invoke();
            onUnpin.Invoke();
        }
        else if (Input.GetButtonDown("Fire3")) {
            onAction3.Invoke();
            onUnpin.Invoke();
        }
    }
}
