using UnityEngine;
using UnityEngine.Events;

public enum Sector {
    Red, Green, Blue
}

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Pie : MonoBehaviour {

    public Color weights {
        get { return cachedRenderer.color; }
        set { cachedRenderer.color = (Vector4)(((Vector3)(Vector4) value).normalized); }
    }

    public UnityEvent onSelfHit, onEnemyHit, onBounce;

    private Transform cachedXf;
    private SpriteRenderer cachedRenderer;

    private void OnTriggerEnter2D(Collider2D other) {
        var otherPie = other.GetComponent<Pie>();
        if (otherPie == null) return;

        var myPoint = cachedXf.position;
        var otherPoint = otherPie.transform.position;

        var mySector = GetSector(otherPoint);
        var otherSector = otherPie.GetSector(myPoint);

        if (mySector == otherSector) {
            onBounce.Invoke();
            return;
        }

        bool morePowerful = mySector == Sector.Red && otherSector == Sector.Green ||
            mySector == Sector.Green && otherSector == Sector.Blue ||
            mySector == Sector.Blue && otherSector == Sector.Red;

        if (morePowerful)
            onSelfHit.Invoke();
        else
            onEnemyHit.Invoke();
    }

    private void Awake() {
        cachedXf = transform;
        cachedRenderer = GetComponent<SpriteRenderer>();
    }

    public Sector GetSector(float angle) {
        var w = weights;

        angle -= cachedXf.rotation.eulerAngles.z;
        angle = Mathf.Repeat(angle + 360.0f, 360.0f) / 360.0f;

        if (angle < w.r) return Sector.Red;
        if (angle >= 1.0f - w.b) return Sector.Blue;

        return Sector.Green;
    }

    public Sector GetSector(Vector2 outerPoint) {
        var toPoint = (Vector3) outerPoint - cachedXf.position;
        return GetSector(Mathf.Atan2(toPoint.y, toPoint.x) * Mathf.Rad2Deg);
    }
}
