using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public int index = 1;

    private void Awake() {
    
    }

    private void Update() {
    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Application.LoadLevel(index);
    }
}
