using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceControl : MonoBehaviour {
    public static RaceControl instance = null;
    public int raceIndex = 0;
    public float goalX = 0;
    [SerializeField]
    private Vehicle[] vehicles;
    private bool someoneWon = false;
    private Vehicle winner = null;

    void Awake() {
        instance = this;
    }

    void Start() {
        UIManager.instance.uiMiniMap.setRaceInfo(vehicles, goalX);
        StartCoroutine(startRace());
    }

    private IEnumerator startRace() {
        yield return new WaitForSeconds(1.0f);

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
        Debug.Log("Someone won!");
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

        UIManager.instance.endUI.GetComponent<Animator>().SetTrigger("showWinLose");

        // player won?
        if(winner.GetComponent<PlayerControl>() != null) {
            UIManager.instance.winElements.SetActive(true);
        } else {
            UIManager.instance.loseElements.SetActive(true);
        }
    }

    public void restartRace() {
        SceneManager.LoadScene("gameplay");
    }
}
