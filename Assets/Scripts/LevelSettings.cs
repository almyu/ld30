using UnityEngine;
using System.Collections;

public class LevelSettings : MonoSingleton<LevelSettings> {

    [System.Serializable]
    public struct Spawn {
        public GameObject prefab;
        public float chance;
    }

    public Spawn[] mapObjects;
    public Spawn[] mapInactiveObjects;
    public Spawn[] enemys;

    public GameObject mapTile;

    public int levelIndex;

    private Material mat;

    private void Awake() {

        var id = Session.homeLevel;

        var player = GameObject.Find("Player/Inner/Visual").transform;

        var pie = GameObject.Find("Player/Pie").GetComponent<SpriteRenderer>();

        var color = PlayerSettings.instance.playerPieColors[id];
        mat = pie.material;
        mat.SetVector("_RedSector", new Vector4(color.r, color.g, color.b, color.a));

        var skin = Instantiate(PlayerSettings.instance.skins[id], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        skin.transform.parent = player;

        foreach (var spriteRenderer in skin.GetComponentsInChildren<SpriteRenderer>())
            ++spriteRenderer.sortingOrder;
    }
}
