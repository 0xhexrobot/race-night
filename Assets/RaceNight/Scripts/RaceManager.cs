using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour {
    public static RaceManager instance = null;
    public float goalX = 0;
    [SerializeField]
    private Vehicle playerVehicle;
    [SerializeField]
    private Vehicle[] aiRivals;
    [SerializeField]
    private GameObject settingsPrefab;
    private Vehicle[] vehicles;
    private bool someoneWon = false;
    private Vehicle winner = null;
    private Settings settings;

    void Awake() {
        instance = this;
    }

    void Start() {
        // create settings object if it doesn't exist
        if(GameObject.Find("settings") != null) {
            settings = GameObject.Find("settings").GetComponent<Settings>();
        } else {
            GameObject settingsObject = Instantiate(settingsPrefab) as GameObject;
            settingsObject.name = "settings";
            settings = settingsObject.GetComponent<Settings>();
        }

        // initialize vehicles
        vehicles = new Vehicle[2];
        vehicles[0] = playerVehicle;
        vehicles[1] = Instantiate(aiRivals[settings.currentRace]);

        UIManager.instance.uiMiniMap.setRaceInfo(vehicles, goalX);
        UIManager.instance.txtLives.text = "x " + settings.livesLeft;

        StartCoroutine(startRace());
    }

    private IEnumerator startRace() {
        Animator txtCounterAnimator = UIManager.instance.txtCounter.GetComponent<Animator>();

        yield return new WaitForSeconds(1.0f);

        int counter = 3;

        do {
            UIManager.instance.txtCounter.text = counter.ToString();
            txtCounterAnimator.SetTrigger("fade");
            counter--;

            yield return new WaitForSeconds(1.25f);
        } while(counter > 0);

        UIManager.instance.txtCounter.text = "GO!";
        txtCounterAnimator.SetTrigger("fade");

        foreach(Vehicle vehicle in vehicles) {
            vehicle.GetComponent<VehicleControl>().startAccel();
        }

        StartCoroutine(checkVehicles());
    }

    private IEnumerator checkVehicles() {
        while(!someoneWon) {
            UIManager.instance.uiMiniMap.updateVehicles();

            foreach(Vehicle vehicle in vehicles) {
                if(vehicle.transform.position.x + 1.0f > goalX) {
                    winner = vehicle;
                    endRace();
                    break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void endRace() {
        someoneWon = true;

        UIManager.instance.normalUi.GetComponent<Animator>().SetTrigger("raceEnd");

        foreach(Vehicle vehicle in vehicles) {
            vehicle.GetComponent<VehicleControl>().stopAcceleration();
            vehicle.stopAccel();
        }

        StartCoroutine(showUi());
    }

    public IEnumerator showUi() {
        yield return new WaitForSeconds(3.0f);

        // player won?
        if(winner.GetComponent<PlayerControl>() != null) {
            UIManager.instance.winElements.SetActive(true);
        } else {
            UIManager.instance.loseElements.SetActive(true);
            settings.livesLeft--;
        }

        UIManager.instance.endUI.GetComponent<Animator>().SetTrigger("showWinLose");
    }

    public void restartRace() {
        if(settings.livesLeft >= 0) {
            SceneManager.LoadScene("gameplay");
        } else {
            SceneManager.LoadScene("title");
        }
    }

    public void toNextRace() {
        if(settings.currentRace + 1 < aiRivals.Length) {
            settings.currentRace++;
            SceneManager.LoadScene("gameplay");
        } else {
            SceneManager.LoadScene("trophy");
        }
    }
}
