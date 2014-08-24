using UnityEngine;
using System.Collections;

public class MapObjects : MonoBehaviour {

    [System.Serializable]
    public struct Spawn {
        public GameObject prefab;
        public float chance;
    }
    
    public static MapObjects instance;

    public Spawn[] prefabs;
    private Transform[] mapObjects;
    
    private Rigidbody2D cachedPlayerRigidbody2D;
    private Transform cachedTransform;

    public float cameraFactor = 1.0f;
    public float speedFactor = 5.0f;
    public float lifeFactor = 8.0f;

    public float minDistance = 10.0f;
    
    public int max = 3;
    
    private void Awake() {
        instance = this;
        
        cachedTransform = transform;
        
        cachedPlayerRigidbody2D = Camera.main.GetComponent<CameraFollow>().target;

        mapObjects = new Transform[max];
        
        var s = 0.0f;
        foreach (var spawn in prefabs)
            s += spawn.chance;
        
        for (int i = 0; i < prefabs.Length; ++i)
            prefabs[i].chance = prefabs[i].chance / s;
    }

    private void Update() {
        var lifeRect = CameraUtility.instance.ScaleRect(lifeFactor);
        for (int i = 0; i < mapObjects.Length; ++i) {
            if (mapObjects[i] != null && !lifeRect.Contains(mapObjects[i].position)) {
                Destroy(mapObjects[i].gameObject);
            }

            if (mapObjects[i] == null) {
                var cameraRect = CameraUtility.instance.ScaleRect(cameraFactor);
                var velocity = cachedPlayerRigidbody2D.velocity;
                var spawnRect = CameraUtility.instance.ScaleRect(speedFactor, velocity);
                
                Vector3 position;
                do {
                    position = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax));
                } while(cameraRect.Contains(position) || CloseObjects(position));
            
                GameObject prefab = prefabs[0].prefab;
                var r = Random.value;
                foreach (var spawn in prefabs) {
                    if (spawn.chance <= r) {
                        r -= spawn.chance;
                        continue;
                    }
                    prefab = spawn.prefab;
                    break;
                }
    
                var rotation = Quaternion.identity;
                rotation.eulerAngles = new Vector3(0.0f, 0.0f, Random.Range(-180.0f, 180.0f));
    
                var mapObject = Instantiate(prefab, position, rotation) as GameObject;
                mapObjects[i] = mapObject.transform;
                mapObjects[i].parent = cachedTransform;
            }
        }
    }

    private bool CloseObjects(Vector3 position) {
        for (int i = 0; i < mapObjects.Length; ++i) {
            if (mapObjects[i] != null) {
                if (Vector3.Distance(mapObjects[i].position, position) <= minDistance) 
                    return true;
            }
        }
        return false;
    }
}
