using UnityEngine;
using UnityEngine.Events;

public class Pin : MonoBehaviour {

    public Pie pie;
    public Sprite unpinnedSprite, pinnedSprite;

    [System.Serializable]
    public class OnPinColorEvent : UnityEvent<Color> {}
    public OnPinColorEvent onPinColor;

    [System.Serializable]
    public class OnPinSectorEvent : UnityEvent<Sector> {}
    public OnPinSectorEvent onPinSector;

    public bool pinned {
        get { return _pinned; }
        set {
            if (_pinned == value) return;

            cachedXf.parent = value ? cachedPieXf : cachedParentXf;
            cachedRenderer.sprite = value ? pinnedSprite : unpinnedSprite;

            foreach (var ctl in cachedControllers)
                ctl.enabled = !value;

            if (value) {
                var sector = pie.GetSector(cachedXf.position);
                onPinSector.Invoke(sector);

                if (cachedCrosshair != null)
                    onPinColor.Invoke(cachedCrosshair.colors[(int) sector]);
            }

            _pinned = value;
        }
    }

    private bool _pinned;
    private Transform cachedXf, cachedParentXf, cachedPieXf;
    private SpriteRenderer cachedRenderer;
    private Crosshair cachedCrosshair;
    private CrosshairController[] cachedControllers;

    private void Awake() {
        cachedXf = transform;
        cachedParentXf = cachedXf.parent;
        cachedPieXf = pie.transform;
        cachedRenderer = GetComponentInChildren<SpriteRenderer>();
        cachedCrosshair = GetComponentInChildren<Crosshair>();
        cachedControllers = GetComponentsInChildren<CrosshairController>();
    }


}
