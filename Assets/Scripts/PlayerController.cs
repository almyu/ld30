﻿using UnityEngine;

[RequireComponent(typeof(Car))]
public class PlayerController : MonoSingleton<PlayerController> {

    private Car cachedCar;

    private void Awake() {
        cachedCar = GetComponent<Car>();
        cachedCar.directControl = PlayerPrefs.GetInt("DirectControl", 0) != 0;
    }

    private void Update() {
        var vaxis = Input.GetAxis("Vertical");
        cachedCar.control = new Vector2(Input.GetAxis("Horizontal"), vaxis * Mathf.Ceil(Mathf.Abs(vaxis)));
    }
}
