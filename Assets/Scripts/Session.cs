using UnityEngine;
using System.Collections.Generic;

public static class Session {

    static Session() {
        // additional initialization stuff
    }

    public static int homeLevel = 0;
    public static float health = 80.0f;

    public static bool isNewGame = true;

    public static Vector3 stats = new Vector3(100.0f, 100.0f, 100.0f);

    public static Vector3 velocity = Vector3.zero;

    public class Dict<T> {

        protected Dictionary<string, T> storage = new Dictionary<string, T>();

        public T Get(string key, T defaultValue) {
            var value = defaultValue;
            storage.TryGetValue(key, out value);
            return value;
        }

        public T Get(string key) {
            return Get(key, default(T));
        }

        public void Set(string key, T value) {
            storage[key] = value;
        }

        public void Clear() {
            storage.Clear();
        }
    }

    public static Dict<bool> bools = new Dict<bool>();
    public static Dict<int> ints = new Dict<int>();
    public static Dict<float> floats = new Dict<float>();
    public static Dict<Vector3> vectors = new Dict<Vector3>();

    public static void ClearDicts() {
        bools.Clear();
        ints.Clear();
        floats.Clear();
        vectors.Clear();
    }
}
