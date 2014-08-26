using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public int course;

    public Transform glow;
    public Transform centerLayer;
    public Transform upLayer;

    public float speedRotation = 1.0f;
    public float speedCenterRotation = 1.0f;
    public float speedUpRotation = 1.0f;

    private void Update() {
        glow.Rotate(Vector3.forward * Time.deltaTime * speedRotation);
        centerLayer.Rotate(Vector3.forward * Time.deltaTime * speedCenterRotation);
        upLayer.Rotate(Vector3.forward * Time.deltaTime * speedUpRotation);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Application.LoadLevel(course + 1);
    }
}
