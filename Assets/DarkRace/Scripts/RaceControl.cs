using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceControl : MonoBehaviour {
    public float goalX = 0;
    [SerializeField]
    private Vehicle[] vehicles;
    private bool someoneWon = false;

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
                if(vehicle.transform.position.x > goalX) {
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

        foreach(Vehicle vehicle in vehicles) {
            vehicle.GetComponent<VehicleControl>().stopAcceleration();
            vehicle.accel = 0;
        }

        //StartCoroutine(restartScene());
    }

    public IEnumerator restartScene() {
        yield return new WaitForSeconds(10.0f);

        SceneManager.LoadScene("gameplay");
    }
}
