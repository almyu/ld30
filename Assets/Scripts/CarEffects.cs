using UnityEngine;

public class CarEffects : MonoBehaviour {

    public GameObject[] prefabs;

    private Transform cachedXf;


    private void Awake() {
        cachedXf = transform;
    }

    public void SpawnPrefab(string prefabName) {
        foreach (var prefab in prefabs) {
            if (prefab.name == prefabName) {
                SpawnPrefab(prefab);
                return;
            }
        }
    }

    public void SpawnPrefab(GameObject prefab) {
        var obj = (GameObject) Instantiate(prefab, cachedXf.position, cachedXf.rotation);
    }

    public void ShowImpactDamage(float force) {
        print(name + " damage: " + force);
    }
}
