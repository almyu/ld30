using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Crosshair : MonoBehaviour {

    public Pie pie;
    public Color[] colors = new[] { Color.red, Color.green, Color.blue };

    private Transform cachedXf;
    private SpriteRenderer cachedRenderer;

    private void Awake() {
        cachedXf = transform;
        cachedRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        cachedRenderer.color = colors[(int) pie.GetSector(cachedXf.position)];
    }
}
