using UnityEngine;
using System.Collections;

public class Sounds : MonoSingleton<Sounds> {

    public AudioSource sourceFenderBender;

    public AudioSource sourceBoost;
    public AudioSource sourceJump;

    public void PlayFenderBender() {
        sourceFenderBender.Play();
    }

    public void PlayBoost() {
        sourceBoost.Play();
    }

    public void PlayJump() {
        sourceJump.Play();
    }
}
