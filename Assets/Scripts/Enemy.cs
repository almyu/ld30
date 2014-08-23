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
        var angle = Vector3.Angle(cachedCar.inner.up, player.position - cachedTransform.position);
        var sign = Mathf.Sign(Vector3.Dot(Vector3.back,Vector3.Cross(cachedCar.inner.up, player.position - cachedTransform.position)));
        cachedCar.control = new Vector2(sign * Mathf.Lerp(0.0f, 1.0f, angle), 1.0f);
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
