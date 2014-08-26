using UnityEngine;
using UnityEngine.Events;

public class Action : MonoBehaviour {

    public string button;
    public float duration = 1.0f;
    public float cooldown = 5.0f;

    public UnityEvent onUse, onEnd, onReady, onFailedUseAttempt;

    [System.Serializable]
    public class OnCooldownChangedEvent : UnityEvent<string> {}
    public OnCooldownChangedEvent onCooldownChanged;

    private float durationTimer = 0.0f;
    private float cooldownTimer = 0.0f;

    private void Update() {
        var newCooldownTimer = Mathf.Max(0.0f, cooldownTimer - Time.deltaTime);
        var newDurationTimer = Mathf.Max(0.0f, durationTimer - Time.deltaTime);

        if (!Mathf.Approximately(newCooldownTimer, cooldownTimer))
            onCooldownChanged.Invoke(newCooldownTimer.ToString());

        if (cooldownTimer >= Mathf.Epsilon && newCooldownTimer < Mathf.Epsilon)
            onReady.Invoke();

        cooldownTimer = newCooldownTimer;


        if (durationTimer >= Mathf.Epsilon && newDurationTimer < Mathf.Epsilon) {
            onEnd.Invoke();
        }

        durationTimer = newDurationTimer;


        if (!Input.GetButton(button)) return;

        if (cooldownTimer >= Mathf.Epsilon) {
            onFailedUseAttempt.Invoke();
            return;
        }

        onUse.Invoke();
        durationTimer = duration;
    }
}
