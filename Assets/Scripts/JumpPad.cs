using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour {

    public AnimationClip clip;

    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.GetComponent<Animator>().SetTrigger("Jump");
        Destroy(gameObject);
    }
}
