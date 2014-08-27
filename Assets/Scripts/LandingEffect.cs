using UnityEngine;

public class LandingEffect : MonoSingleton<LandingEffect> {

	public GameObject prefab;

	public void Spawn() {
		PlayerController.instance.GetComponent<CarEffects>().SpawnPrefab(prefab);
	}

}
