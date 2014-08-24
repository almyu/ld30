using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Transform neonSkin;
    public Transform rustSkin;
    public Transform woodSkin;

    public RectTransform neonButton;
    public RectTransform rustButton;
    public RectTransform woodButton;

    private void Awake() {
        PlayerPrefs.SetFloat("RustPoints", 100.0f);
        PlayerPrefs.SetFloat("NeonPoints", 100.0f);
        PlayerPrefs.SetFloat("WoodenPoints", 100.0f);
    }

    private void Update() {
        neonSkin.position = new Vector3(Camera.main.ScreenToWorldPoint(neonButton.position).x, 0.0f, 0.0f);
        rustSkin.position = new Vector3(Camera.main.ScreenToWorldPoint(rustButton.position).x, 0.0f, 0.0f);
    }

    public void LoadLevel(int id) {
        PlayerPrefs.SetInt("HomeLevel", id);
        Application.LoadLevel(id);
    }

    public void ChangeMod(bool active) {
        PlayerPrefs.SetInt("DirectControl", active ? 1 : 0);
    }
}
