﻿using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour {

    public AnimationClip clip;

    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.animation.clip = clip;
        other.gameObject.animation.Play();
        Destroy(gameObject);
    }
}