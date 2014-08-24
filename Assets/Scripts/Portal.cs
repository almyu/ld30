using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public LevelSettings.LevelName course;

    public Transform glow;

    public float speedRotation = 1.0f;

    private void Awake() {
    
    }

    private void Update() {
        glow.Rotate(Vector3.forward * Time.deltaTime * speedRotation);
    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        int index = 0;
        switch (course) {
            case LevelSettings.LevelName.RustEmpire : index = 0; break;
            case LevelSettings.LevelName.NeonRepublic : index = 1; break;
            case LevelSettings.LevelName.WoodenKingdom : index = 2; break;
        }
        Application.LoadLevel(index);
    }
}
