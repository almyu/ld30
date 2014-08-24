using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    
    public float lifeDistance = 30.0f;
    
    private Transform player;
    private Transform cachedTransform;
    private Transform cachedTransformCamera;
    private Car cachedCar;
    
    private bool isLive = true;

    private void Awake() {
        cachedTransform = transform;
        cachedTransformCamera = Camera.main.transform;
        
        cachedCar = GetComponent<Car>();
    }
    
    private void Start() {
        player =  PlayerController.instance.transform;
    }
    
    private void Update() {
        cachedCar.control = (player.position - cachedTransform.position).normalized;
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
