using UnityEngine;
using System.Collections;

public class CameraUtility : MonoBehaviour {

	public static CameraUtility instance;

	public float width, height;

	public Rect rectVisibility;

	private Transform cachedTransform;

    private void Awake() {
    	instance = this;

    	height = 2f * Camera.main.orthographicSize;
    	width = height * Camera.main.aspect;

    	cachedTransform = Camera.main.transform;
    }

	public Vector2 position {
		get {
			return new Vector2(cachedTransform.position.x, cachedTransform.position.y);
		}
	}

	public Rect rect {
        get {
            return new Rect(-width / 2.0f, -height / 2.0f, width, height);
        }
    }

    public Rect ScaleRect(float scale) {
        
        return ScaleRect(scale, Vector2.zero);
    }

    public Rect ScaleRect(float scale, Vector2 offset) {
    	var scaleRect = rect;
        scaleRect.size = new Vector2(rect.width * scale, rect.height * scale);
        scaleRect.position = position - scaleRect.size / 2.0f + offset;
        
        return scaleRect;
    }
}
