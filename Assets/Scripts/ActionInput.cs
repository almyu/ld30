using UnityEngine;
using UnityEngine.Events;

public class ActionInput : MonoBehaviour {

    public UnityEvent onPin;

    private void Update() {
        if (Input.GetMouseButtonDown(0))
            onPin.Invoke();
    }
}
