using UnityEngine;
using System.Collections;

public class LevelSettings : MonoSingleton<LevelSettings> {

    [System.Serializable]
    public struct Spawn {
        public GameObject prefab;
        public float chance;
    }

    public GameObject[] skins;

    public Spawn[] mapObjects;
    public Spawn[] enemys;

    public GameObject mapTile;

    public int levelIndex;

    private void Awake() {

        var id = PlayerPrefs.GetInt("HomeLevel", 0);

        var player = GameObject.Find("Player");
        var inner = player.GetComponent<Car>().inner;

        var skin = Instantiate(skins[id], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        skin.transform.parent = inner;
    }
}
