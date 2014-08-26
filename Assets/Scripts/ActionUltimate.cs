using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActionUltimate : MonoBehaviour {

    public LayerMask enemyLayers = -1;
    public float damage = 10.0f;

    private void OnTriggerStay2D(Collider2D coll) {
        if (((1 << coll.gameObject.layer) & enemyLayers.value) == 0) return;

        var car = coll.GetComponent<Car>();
        if (car == null) return;

        car.Hit(damage);
    }
}
