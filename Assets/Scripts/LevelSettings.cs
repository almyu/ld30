using UnityEngine;
using System.Collections;

public class LevelSettings : MonoBehaviour {

    public static LevelSettings instance;

    public GameObject[] skins;

    public enum LevelName{
        RustEmpire,
        NeonRepublic,
        WoodenKingdom
    };

    public LevelName levelName;
    public int levelIndex;

    private void Awake() {
        instance = this;

        switch (levelName) {
            case LevelSettings.LevelName.RustEmpire : levelIndex = 0; break;
            case LevelSettings.LevelName.NeonRepublic : levelIndex = 1; break;
            case LevelSettings.LevelName.WoodenKingdom : levelIndex = 2; break;
        }

        var id = PlayerPrefs.GetInt("HomeLevel", 0);

        var player = GameObject.Find("Player");
        var inner = player.GetComponent<Car>().inner;

        var skin = Instantiate(skins[id], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        skin.transform.parent = inner;
    }
}
