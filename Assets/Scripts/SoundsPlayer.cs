using UnityEngine;
using System.Collections;

public class SoundsPlayer : MonoSingleton<SoundsPlayer> {

    public AudioSource engine;

    public AudioSource impact;

    private void Awake() {
        engine.clip = PlayerSettings.instance.engineClip[Session.homeLevel];
        //engine.Play();
    }

    private void Update() {
        engine.volume = Mathf.Abs(Input.GetAxis("Vertical"));
    }

    public void PlayImpact() {
        impact.Play();
    }
}
