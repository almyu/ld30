using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public EventSystem system;
    public GameObject startSelected;

    public Toggle toggle;

    public Transform neonSkin;
    public Transform rustSkin;
    public Transform woodSkin;

    public RectTransform neonButton;
    public RectTransform rustButton;
    public RectTransform woodButton;

    private void Awake() {
        BaseEventData pointer = new BaseEventData(system);
        pointer.selectedObject = startSelected;
        system.SetSelectedGameObject(startSelected, pointer);

        PlayerPrefs.SetInt("NewGame", 1);

        toggle.isOn = PlayerPrefs.GetInt("DirectControl", 0) == 1;
    }

    private void Update() {
        neonSkin.position = new Vector3(Camera.main.ScreenToWorldPoint(neonButton.position).x, 0.0f, 0.0f);
        rustSkin.position = new Vector3(Camera.main.ScreenToWorldPoint(rustButton.position).x, 0.0f, 0.0f);
        woodSkin.position = new Vector3(Camera.main.ScreenToWorldPoint(woodButton.position).x, 0.0f, 0.0f);
    }

    public void LoadLevel(int id) {
        Session.homeLevel = id;

        Application.LoadLevel(id + 1);
    }

    public void ChangeMod(bool active) {
        PlayerPrefs.SetInt("DirectControl", active ? 1 : 0);
    }
}
