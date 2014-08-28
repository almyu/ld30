using UnityEngine;

public class ParticleTweaker : MonoBehaviour {

    public float sizeFactor = 1.0f;

    private ParticleSystem[] cachedSystems;
    private float[] cachedSizes;

    private void Start() {
        cachedSystems = GetComponentsInChildren<ParticleSystem>();
        cachedSizes = System.Array.ConvertAll(cachedSystems, ps => ps.startSize);
    }

    private void Update() {
        for (int i = 0; i < cachedSystems.Length; ++i)
            cachedSystems[i].startSize = cachedSizes[i] * sizeFactor;
    }
}
