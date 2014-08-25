using UnityEngine;

public static class Session {

    static Session() {
        // additional initialization stuff
    }

    public static float stuff = 42.0f;

    public static int homeLevel = 0;
    public static float helth = 10.0f;

    public static bool isNewGame = true;

    public static Vector3 stats = new Vector3(100.0f, 100.0f, 100.0f);

    public static Vector3 velocity = Vector3.zero;
}
