using UnityEngine;
using System.Collections;

public class Spawns : MonoSingleton<Spawns> {    

    public float lifeDistance = 20.0f;
    public float aggressionDistance = 5.0f;
    
    private Rigidbody2D cachedPlayerRigidbody2D;
    private Transform cachedTransform;

    public float cameraFactor = 1.0f;
    public float speedFactor = 5.0f;
    public float lifeFactor = 3.0f;

    public Rect lifeRect;
    
    public int max = 10;
    public int current = 0;
    
    private void Awake() {
        
        cachedTransform = transform;
        
        var s = 0.0f;
        foreach (var spawn in LevelSettings.instance.enemys)
            s += spawn.chance;
        
        for (int i = 0; i < LevelSettings.instance.enemys.Length; ++i)
            LevelSettings.instance.enemys[i].chance = LevelSettings.instance.enemys[i].chance / s;

        var id = PlayerPrefs.GetInt("HomeLevel", 0);
        if (id == LevelSettings.instance.levelIndex)
            aggressionDistance = 0.0f;
    }

    private void Start() {
        cachedPlayerRigidbody2D = Camera.main.GetComponent<CameraFollow>().target;
    }

    private void Update() {
        lifeRect = CameraUtility.instance.ScaleRect(lifeFactor);
        while (current < max) {
            var cameraRect = CameraUtility.instance.ScaleRect(cameraFactor);
            var velocity = cachedPlayerRigidbody2D.velocity;
            var spawnRect = CameraUtility.instance.ScaleRect(speedFactor, velocity);
            
            Vector3 position;
            do {
                position = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax));
            } while(cameraRect.Contains(position));
            
            GameObject prefab = LevelSettings.instance.enemys[0].prefab;
            var r = Random.value;
            foreach (var spawn in LevelSettings.instance.enemys) {
                if (spawn.chance <= r) {
                    r -= spawn.chance;
                    continue;
                }
                prefab = spawn.prefab;
                break;
            }

            var rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0.0f, 0.0f, Random.Range(-180.0f, 180.0f));

            var enemy = Instantiate(prefab, position, Quaternion.identity) as GameObject;
            enemy.transform.parent = cachedTransform;
            enemy.GetComponent<Car>().inner.rotation = rotation;
            current++;
        }
    }
}
