using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    
    public int fraction = 0;

    private Transform player;
    private Transform cachedTransform;
    private Car cachedCar;

    public float killPoint = 7.0f;

    public float delay = 1.0f;
    private float delayTimer = 0.0f;

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
            delayTimer = delay;
        }
        else {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0.0f) {
                var dir = (player.position - cachedTransform.position).normalized;
                cachedCar.control = new Vector3(Mathf.Lerp(cachedCar.inner.up.x, dir.x, Time.deltaTime * cachedCar.turningSpeed),
                                                Mathf.Lerp(cachedCar.inner.up.y, dir.y, Time.deltaTime * cachedCar.turningSpeed),
                                                0.0f);
                time = 0.0f;
            }
        }

        if (!Spawns.instance.lifeRect.Contains(cachedTransform.position))
            Remove();
    }
    
    private void Remove() {
         Destroy(gameObject);
    }

    private void OnDestroy() { 
        Spawns.instance.current--;
    }
}
