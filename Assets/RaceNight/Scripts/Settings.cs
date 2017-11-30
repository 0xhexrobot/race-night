using UnityEngine;

public class Settings : MonoBehaviour {
    public static Settings instance = null;
    public int currentRace = 0;
    public int livesLeft = 0;

    void Awake() {
        instance = this;
    }

    void Start() {
        DontDestroyOnLoad(this);
    }

    public void resetLives() {
        livesLeft = 2;
    }
}
