using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : VehicleControl {
    private float lastFloorX = 20;
    private float accumAxis = 0;
    private float accumAxisChange = 0.05f;
    private Transmission transmission;
    private float tolerance;

    void Start() {
        transmission = GetComponent<Transmission>();
        updateTransmissionUi();
    }

    public override IEnumerator accelerate() {
        accelerating = true;

        yield return new WaitForSeconds(1.0f);

        while(accelerating) {
            float accelPercent = (1.0f - Mathf.Abs(transmission.transmissionValue)) / (1.0f - tolerance);
            accelPercent = Mathf.Min(accelPercent, 1.0f);

            GetComponent<Vehicle>().updateAccelFactor(accelPercent);

            transmission.update();

            float axis = Input.GetAxis("Horizontal");

            accumAxis += accumAxisChange * axis;
            accumAxis = Mathf.Clamp(accumAxis, -2.0f, 2.0f);
            transmission.applyAxis(accumAxis);

            if(Mathf.Abs(accumAxis) > 0.01f) {
                accumAxis *= 0.98f;
            } else {
                accumAxis = 0;
            }

            UIManager.instance.uiTransmission.setValue(transmission.transmissionValue);

            float velocity = GetComponent<Vehicle>().velocity;
            int velocityKm = Mathf.RoundToInt(velocity * 3.6f);
            // int velocityMi = Mathf.RoundToInt(velocity * 2.23f);

            yield return new WaitForEndOfFrame();

            bool nextPhaseReady = transmission.getVelocityToNextPhase() != -1 && velocity > transmission.getVelocityToNextPhase();
            int nextPhase = transmission.getCurrentPhase() + 2;

            //update ui
            UIManager.instance.uiTransmission.showLblReady(nextPhaseReady);
            UIManager.instance.uiTransmission.setLblReadyPhase(nextPhase);
            UIManager.instance.txtVelocity.text = velocityKm.ToString();
        }
    }

    void Update() {
        if(Input.GetButtonDown("Fire1")) {
            bool changedPhase = GetComponent<Vehicle>().tryToChangeTransmissionPhase();

            if(changedPhase) {
                updateTransmissionUi();
            }
        }

        //create new floor
        if(lastFloorX - transform.position.x < 20) {
            string prefabName;
            lastFloorX += 20;

            if(lastFloorX == RaceManager.instance.goalX) {
                prefabName = "floorLine";
            } else {
                prefabName = "floor";
            }

            GameObject floorObject = ObjectPool.instance.getObjectForType(prefabName);
            floorObject.transform.position = new Vector3(lastFloorX, 0, 0);
        }
    }

    private void updateTransmissionUi() {
        tolerance = transmission.getTransmissionPhase().tolerance;
        UIManager.instance.uiTransmission.setTolerance(tolerance);
        UIManager.instance.uiTransmission.setPhaseNumber(transmission.getCurrentPhase() + 1);
    }
}
