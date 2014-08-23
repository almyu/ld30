using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    
    public float lifeDistance = 30.0f;
    
    private Transform cachedTransform;
    private Transform cachedTransformCamera;
    
    private bool isLive = true;

    private void Awake() {
        cachedTransform = transform;
        cachedTransformCamera = Camera.main.transform;
    }
    
    private void Update() {
        if (Vector3.Distance(cachedTransform.position, cachedTransformCamera.position) >= lifeDistance)
            Remove();
    }
    
    private void OnBecameInvisible() {
        if (isLive)
            Remove();
    }
    
    private void Remove() {
         isLive = false;
         Destroy(gameObject);
         Spawns.instance.current--;
    }
}
