using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    void Start() {
        if(GameObject.Find("settings") != null) {
            Settings settings = GameObject.Find("settings").GetComponent<Settings>();
            settings.currentRace = 0;
            settings.resetLives();
        }
    }

    void Update() {
        if(Input.GetButton("Submit") || Input.GetButton("Fire1")) {
            SceneManager.LoadScene("gameplay");
        }
    }
}
