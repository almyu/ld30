using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public float explosionRadius = 3.0f;
    public float force = 5.0f;

    public AudioSource source;

    private Transform cachedTransform;

    private void Awake() {
        cachedTransform = transform;
    }

    public void Execute() {
        source.Play();
        var enemys = Spawns.instance.GetComponentsInChildren<Rigidbody2D>();
        foreach (var enemy in enemys) {
            if (Vector3.Distance(enemy.transform.position, cachedTransform.position) <= explosionRadius) {
                enemy.AddForce((enemy.transform.position - cachedTransform.position).normalized * force, ForceMode2D.Impulse);
                var car = enemy.GetComponent<Car>();
                car.Hit(30.0f);
            }
        }
    }
}
