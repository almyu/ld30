using UnityEngine;
using System.Collections;

public class BoostPad : MonoBehaviour {

    public AnimationClip clip;

    public Transform icons;
    private Transform player;

    private void Start() {
    	player = GameObject.Find("Player").transform;
    }

    private void Update() {
    	icons.up = -(player.position - icons.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.animation.clip = clip;
        other.gameObject.animation.Play();
        Destroy(gameObject);
    }
}
