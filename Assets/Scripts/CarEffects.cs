using UnityEngine;

public class CarEffects : MonoBehaviour {

    public GameObject sparksPrefab;

    private Transform cachedXf;


    private void Awake() {
        cachedXf = transform;
    }

    public void SpawnPrefab(GameObject prefab) {
        Instantiate(prefab, cachedXf.position, cachedXf.rotation);
    }

    public void SpawnSparks(float force, Collision2D collision) {
        var rotation = Quaternion.LookRotation(collision.relativeVelocity.normalized, Vector3.back);
        var obj = (GameObject) Instantiate(sparksPrefab, collision.contacts[0].point, rotation);
        obj.GetComponent<ParticleSystem>().maxParticles = Mathf.CeilToInt(force * 10.0f);
        Destroy(obj, 1.0f);
    }
}
