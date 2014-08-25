using UnityEngine;

public class CarEffects : MonoBehaviour {

    public GameObject sparksPrefab;

    private Transform cachedXf;


    private void Awake() {
        cachedXf = transform;
    }

    public void SpawnPrefab(GameObject prefab) {
        var obj = (GameObject) Instantiate(prefab, cachedXf.position, cachedXf.rotation);

        var pss = obj.GetComponentsInChildren<ParticleSystem>();
        if (pss.Length != 0) {
            var lifetime = 0.0f;

            foreach (var ps in pss)
                if (!ps.loop)
                    if (lifetime < ps.duration)
                        lifetime = ps.duration;

            if (lifetime > 0.0f)
                Destroy(obj, lifetime);
        }
    }

    public void SpawnSparks(float force, Collision2D collision) {
        var rotation = Quaternion.LookRotation(collision.relativeVelocity.normalized, Vector3.back);
        var obj = (GameObject) Instantiate(sparksPrefab, collision.contacts[0].point, rotation);
        var ps = obj.GetComponent<ParticleSystem>();

        ps.maxParticles = Mathf.CeilToInt(force * 10.0f);
        Destroy(obj, ps.duration);
    }
}
