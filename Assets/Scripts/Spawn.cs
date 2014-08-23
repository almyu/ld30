using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject prefab;
    
    private Rigidbody2D cachedPlayerRigidbody2D;
    private Transform cachedTransform;
    private Transform cachedTransformCamera;

    public float speedFactor = 5.0f;
    public float cameraFactor = 1.0f;
    
    public float max = 10;
    public float current = 0;
    
    private void Awake() {
        cachedTransformCamera = Camera.main.transform;
        cachedTransform = transform;
    }

    private void Update() {
        while (current < max) {
            var camHeight = 2f * Camera.main.orthographicSize;
            var camWidth = camHeight * Camera.main.aspect;
            var velocity = Vector3.right + Vector3.down;
            var camCornerCenter = Vector3.right * camWidth / 2.0f * Mathf.Sign(velocity.x) + Vector3.up * camHeight / 2.0f  * Mathf.Sign(velocity.y);
            var camCornerLeft = -Vector3.right * camWidth / 2.0f * Mathf.Sign(velocity.x) + Vector3.up * camHeight / 2.0f  * Mathf.Sign(velocity.y);
            var camCornerRight = Vector3.right * camWidth / 2.0f * Mathf.Sign(velocity.x) - Vector3.up * camHeight / 2.0f  * Mathf.Sign(velocity.y);
            var velocityAdd = new Vector3(velocity.x * speedFactor, velocity.y * speedFactor, 0.0f);
            var cameraFactorAdd = new Vector3(velocity.x * cameraFactor, velocity.y * cameraFactor, 0.0f);
            var camAdd = Vector3.zero;
            if (Random.Range(0,2) == 1) {
                camAdd = new Vector3(Random.Range((cameraFactorAdd + camCornerLeft).x, (cameraFactorAdd + camCornerCenter + velocityAdd).x),
                    Random.Range((cameraFactorAdd + camCornerLeft).y, (cameraFactorAdd + camCornerLeft + velocityAdd).y),
                    0.0f);
            }
            else {
                camAdd = new Vector3(Random.Range((cameraFactorAdd + camCornerRight).x, (cameraFactorAdd + camCornerRight + velocityAdd).x),
                    Random.Range((cameraFactorAdd + camCornerRight).y, (cameraFactorAdd + camCornerCenter + velocityAdd).y),
                    0.0f);
            }
            var enemy = Instantiate(prefab, cachedTransformCamera.position + camAdd, Quaternion.identity) as GameObject;
            enemy.transform.parent = cachedTransform;
            current++;
        }
    }
}
