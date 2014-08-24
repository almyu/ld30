using UnityEngine;
using System.Collections;

public class Spawns : MonoBehaviour {

    [System.Serializable]
    public struct Spawn {
        public GameObject prefab;
        public float chance;
    }
    
    public static Spawns instance;

    public Spawn[] spawns;
    
    private Rigidbody2D cachedPlayerRigidbody2D;
    private Transform cachedTransform;
    private Transform cachedTransformCamera;

    public float cameraFactor = 1.0f;
    public float speedFactor = 5.0f;
    
    public float max = 10;
    public float current = 0;
    
    private void Awake() {
        instance = this;
        
        cachedTransformCamera = Camera.main.transform;
        cachedTransform = transform;
        
        cachedPlayerRigidbody2D = Camera.main.GetComponent<CameraFollow>().target;
        
        var s = 0.0f;
        foreach (var spawn in spawns)
            s += spawn.chance;
        
        for (int i = 0; i < spawns.Length; ++i)
            spawns[i].chance = spawns[i].chance / s;
    }

    private void Update() {
        while (current < max) {
            var cameraRect = CameraUtility.instance.ScaleRect(cameraFactor);
            var velocity = cachedPlayerRigidbody2D.velocity;
            var spawnRect = CameraUtility.instance.ScaleRect(speedFactor, velocity);
            
            Vector3 camAdd;
            do {
                camAdd = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax));
            } while(cameraRect.Contains(camAdd));
            
            GameObject prefab = spawns[0].prefab;
            var r = Random.value;
            foreach (var spawn in spawns) {
                if (spawn.chance <= r) {
                    r -= spawn.chance;
                    continue;
                }
                prefab = spawn.prefab;
                break;
            }
                
            var enemy = Instantiate(prefab, camAdd, Quaternion.identity) as GameObject;
            enemy.transform.parent = cachedTransform;
            current++;
        }
    }
}
