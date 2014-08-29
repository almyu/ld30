using UnityEngine;
using System.Collections;

public class Cheat : MonoBehaviour {

    #if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyDown("o"))
            Session.stats.x = 1000.0f;
        if (Input.GetKeyDown("p"))
            Session.stats.x = 0.0f;
    }
    #endif
}
