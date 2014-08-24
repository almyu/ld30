using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    
    private Transform player;
    private Transform cachedTransform;
    private Car cachedCar;

    private float time = 0.0f;

    private float angle;

    private void Awake() {
        cachedTransform = transform;
        
        angle = Random.Range(2.1f, 4.1f);

        cachedCar = GetComponent<Car>();
    }
    
    private void Start() {
        player =  PlayerController.instance.transform;
    }
    
    private void Update() {
        time = (time + Time.deltaTime) % angle;
        var distance = Vector3.Distance(cachedTransform.position, player.position);

        if (distance >= Spawns.instance.aggressionDistance) {
            cachedCar.control = ((time - angle / 2.0f) * cachedCar.inner.right + cachedCar.inner.up).normalized;
        }
        else {
            cachedCar.control = (player.position - cachedTransform.position).normalized;
            time = 0.0f;
        }

        if (distance >= Spawns.instance.lifeDistance)
            Remove();
    }
    
    private void Remove() {
         Destroy(gameObject);
         Spawns.instance.current--;
    }
}
