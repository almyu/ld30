using UnityEngine;
using System.Collections;

public class LevelSettings : MonoBehaviour {

    public static LevelSettings instance;

    public enum LevelName{
        RustEmpire,
        NeonRepublic,
        WoodenKingdom
    };

    public LevelName levelName;

    private void Awake() {
        instance = this;
    }
}
