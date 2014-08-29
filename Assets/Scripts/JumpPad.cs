using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.isTrigger) return;

        Sounds.instance.PlayJump();
        other.gameObject.GetComponent<Animator>().SetTrigger("Jump");
        Destroy(gameObject);
    }
}
