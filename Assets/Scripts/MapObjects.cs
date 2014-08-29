using UnityEngine;
using System.Collections;

public class MapObjects : MonoSingleton<MapObjects> {

    private Transform[] mapObjects;
    private Transform[] mapInactiveObjects;
    
    private Rigidbody2D cachedPlayerRigidbody2D;
    private Transform cachedTransform;

    public float cameraFactor = 1.0f;
    public float speedFactor = 5.0f;
    public float lifeFactor = 8.0f;

    public float minDistance = 10.0f;

    private void Awake() {
        cachedTransform = transform;

        var cameraRect = CameraUtility.instance.ScaleRect(cameraFactor);
        var spawnRect = CameraUtility.instance.ScaleRect(speedFactor);

        var sc = minDistance * minDistance * Mathf.PI / 3.5f;
        var max = (spawnRect.width * spawnRect.height - cameraRect.width * cameraRect.height) / sc;

        mapObjects = new Transform[(int) max];

        mapInactiveObjects = new Transform[(int) (max / 2)];
    }
        
    private void Start() {
        cachedPlayerRigidbody2D = Camera.main.GetComponent<CameraFollow>().target;

        var s = 0.0f;
        foreach (var spawn in LevelSettings.instance.mapObjects)
            s += spawn.chance;
        
        for (int i = 0; i < LevelSettings.instance.mapObjects.Length; ++i)
            LevelSettings.instance.mapObjects[i].chance = LevelSettings.instance.mapObjects[i].chance / s;

        s = 0.0f;
        foreach (var spawn in LevelSettings.instance.mapInactiveObjects)
            s += spawn.chance;
        
        for (int i = 0; i < LevelSettings.instance.mapInactiveObjects.Length; ++i)
            LevelSettings.instance.mapInactiveObjects[i].chance = LevelSettings.instance.mapInactiveObjects[i].chance / s;
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
            
                var prefab = LevelSettings.instance.mapObjects[0].prefab;
                var rotationRange = Vector2.zero;

                var r = Random.value;
                foreach (var spawn in LevelSettings.instance.mapObjects) {
                    if (spawn.chance <= r) {
                        r -= spawn.chance;
                        continue;
                    }
                    prefab = spawn.prefab;
                    rotationRange = spawn.constrainRotation ? spawn.rotationConstraint : new Vector2(-180.0f, 180.0f);
                    break;
                }
    
                var rotation = Quaternion.AngleAxis(Random.Range(rotationRange[0], rotationRange[1]), Vector3.back);
    
                var mapObject = Instantiate(prefab, position, rotation) as GameObject;
                mapObjects[i] = mapObject.transform;
                mapObjects[i].parent = cachedTransform;
            }
        }

        for (int i = 0; i < mapInactiveObjects.Length; ++i) {
            if (mapInactiveObjects[i] != null && !lifeRect.Contains(mapInactiveObjects[i].position)) {
                Destroy(mapInactiveObjects[i].gameObject);
            }

            if (mapInactiveObjects[i] == null) {
                var cameraRect = CameraUtility.instance.ScaleRect(cameraFactor);
                var velocity = cachedPlayerRigidbody2D.velocity;
                var spawnRect = CameraUtility.instance.ScaleRect(speedFactor, velocity);
                
                Vector3 position;
                do {
                    position = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax));
                } while(cameraRect.Contains(position) || CloseInactiveObjects(position));
            
                GameObject prefab = LevelSettings.instance.mapInactiveObjects[0].prefab;
                var r = Random.value;
                foreach (var spawn in LevelSettings.instance.mapInactiveObjects) {
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
                mapInactiveObjects[i] = mapObject.transform;
                mapInactiveObjects[i].parent = cachedTransform;
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

    private bool CloseInactiveObjects(Vector3 position) {
        for (int i = 0; i < mapInactiveObjects.Length; ++i) {
            if (mapInactiveObjects[i] != null) {
                if (Vector3.Distance(mapInactiveObjects[i].position, position) <= minDistance) 
                    return true;
            }
        }
        return false;
    }
}
