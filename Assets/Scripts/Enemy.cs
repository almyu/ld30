using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private void Awake() {
    
    }

    private void Update() {
    
    }
    
    private void OnBecameInvisible() {
        Destroy(gameObject);
        Spawns.instance.current--;
    }
}
