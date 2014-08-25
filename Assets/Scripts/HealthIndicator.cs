using UnityEngine;
using System.Collections;

public class HealthIndicator : MonoBehaviour {

    public SpriteRenderer pie;

    private Car cachedCar;
    private float max; //TODO 

    private void Awake() {
        cachedCar = GetComponent<Car>();
        max = cachedCar.health;

        var id = PlayerPrefs.GetInt("HomeLevel", 0);

        var color = PlayerSettings.instance.playerPieColors[id];
        pie.sharedMaterial.SetVector("_RedSector", new Vector4(color.r, color.g, color.b, color.a));
    }

    private void Update() {
        pie.color = pie.color.ReplaceR(cachedCar.health / max);
    }
}
