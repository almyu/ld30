using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSorter : MonoBehaviour {

    public string sortingLayerName;

    private void Awake() {
        particleSystem.renderer.sortingLayerName = sortingLayerName;
    }
}
