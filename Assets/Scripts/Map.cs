using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public GameObject prefab;
    
    public float cameraFactor = 1.4f;
    public float tileSize = 2.0f;
    
    private Transform[] maps;
    
    private Transform cachedTransformCamera;
    private Rigidbody2D cachedPlayerRigidbody2D;

    private void Awake() {
        cachedTransformCamera = Camera.main.transform;
        cachedPlayerRigidbody2D = Camera.main.GetComponent<CameraFollow>().target;
        
        var cameraRect = CameraRect(cameraFactor);
        
        var tempMaps = new List<Transform>();
    
        for (var x = cameraRect.xMin; x <= cameraRect.xMax; x += tileSize) {
            for (var y = cameraRect.yMin; y <= cameraRect.yMax; y += tileSize) {
                tempMaps.Add(Instantiate(prefab, new Vector3(x, y, 0.0f), Quaternion.identity) as Transform);
            }
        }
    
        maps = tempMaps.ToArray();
    }

    private void Update() {

    }
    
    private Rect CameraRect(float factor) {
        var camHeight = 2f * Camera.main.orthographicSize;
        var camWidth = camHeight * Camera.main.aspect;
        var velocity = cachedPlayerRigidbody2D.velocity;
        var position = new Vector2(cachedTransformCamera.position.x, cachedTransformCamera.position.y);
        var cameraRect = new Rect(-camWidth / 2.0f, -camHeight / 2.0f, camWidth, camHeight);
        cameraRect.size = new Vector2(camWidth * factor, camHeight * factor);
        cameraRect.position = position - cameraRect.size / 2.0f;
        
        return cameraRect;
    }
}
