using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public float explosionRadius = 3.0f;
    public float force = 5.0f;

    private Transform cachedTransform;

    private void Awake() {
        cachedTransform = transform;
    }

    private void Update() {
    
    }

    public void Execute() {
        var enemys = Spawns.instance.GetComponentsInChildren<Rigidbody2D>();
        foreach (var enemy in enemys) {
            if (Vector3.Distance(enemy.transform.position, cachedTransform.position) <= explosionRadius) {
                enemy.AddForce((enemy.transform.position - cachedTransform.position).normalized * force, ForceMode2D.Impulse);
                Debug.Log((enemy.transform.position - cachedTransform.position).normalized * force);
            }
        }
    }
}
