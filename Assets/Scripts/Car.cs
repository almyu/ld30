﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Car : MonoBehaviour {

    public float acceleration = 20.0f;

    private Rigidbody2D cachedBody;

    private void Awake() {
        cachedBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        var dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        cachedBody.AddForce(dir * Time.fixedDeltaTime * acceleration, ForceMode2D.Impulse);
    }
}