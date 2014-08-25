using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public int course;

    public Transform glow;

    public float speedRotation = 1.0f;

    private void Update() {
        glow.Rotate(Vector3.forward * Time.deltaTime * speedRotation);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Application.LoadLevel(course);
    }
}
