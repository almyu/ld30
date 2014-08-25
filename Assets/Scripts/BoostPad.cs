using UnityEngine;
using System.Collections;

public class BoostPad : MonoBehaviour {
    
    public Transform icons;
    private Transform player;
    private Animator cachedAnimator;

    private void Start() {
        player = GameObject.Find("Player").transform;
        cachedAnimator = player.GetComponent<Animator>();
    }

    private void Update() {
        icons.up = -(player.position - icons.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        cachedAnimator.SetTrigger("Boost");
        Destroy(gameObject);
    }
}
