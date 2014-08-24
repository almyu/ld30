using UnityEngine;

public class Wheels : MonoBehaviour {

    public Transform[] turningWheels;
    public float turnAngle = 30.0f;
    public Sprite spinningWheelSprite, stillWheelSprite;

    public float turn {
        get { return _turn; }
        set {
            _turn = value;

            foreach (var wheel in turningWheels) {
                wheel.localRotation = Quaternion.identity;
                wheel.Rotate(Vector3.back, _turn * turnAngle);
            }
        }
    }

    public bool spinning {
        get { return spinning; }
        set {
            _spinning = value;

            var sprite = value ? spinningWheelSprite : stillWheelSprite;
            foreach (var ren in cachedRenderers)
                ren.sprite = sprite;
        }
    }

    private float _turn;
    private bool _spinning;
    private SpriteRenderer[] cachedRenderers;

    private void Awake() {
        cachedRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
}
