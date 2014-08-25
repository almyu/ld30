using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour {

    private void Awake() {
    
    }

    private void Update() {
    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.animation.Play();
    }
}
