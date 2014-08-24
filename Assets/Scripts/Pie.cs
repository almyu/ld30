using UnityEngine;

public enum Sector {
    Red, Green, Blue
}

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Pie : MonoBehaviour {

    public Color weights {
        get { return cachedRenderer.color; }
        set { cachedRenderer.color = (Vector4)(((Vector3)(Vector4) value).normalized); }
    }

    private Transform cachedXf;
    private SpriteRenderer cachedRenderer;

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
