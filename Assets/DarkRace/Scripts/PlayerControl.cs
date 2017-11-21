using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : VehicleControl {
    private float lastFloorX = 20;
    private float accumAxis = 0;
    private float accumAxisChange = 0.05f;
    private Transmission transmission;
    private float tolerance = 0.1f;

    void Start() {
        transmission = new Transmission();
        UIManager.instance.uiTransmission.setTolerance(tolerance);
    }

    public override IEnumerator accelerate() {
        accelerating = true;

        yield return new WaitForSeconds(1.0f);

        while(accelerating) {
            float accelPercent = (1.0f - Mathf.Abs(transmission.transmissionValue)) / (1.0f - tolerance);
            accelPercent = Mathf.Min(accelPercent, 1.0f);

            Debug.LogFormat("Transmission: {0}, accel %: {1}", transmission.transmissionValue, accelPercent);

            GetComponent<Vehicle>().accelerate(accelPercent);

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
            //km per h
            int realVelocity = Mathf.RoundToInt(GetComponent<Vehicle>().velocity * 3.6f);
            UIManager.instance.txtVelocity.text = realVelocity.ToString();

            yield return new WaitForEndOfFrame();
        }
    }

    void Update() {
        //create new floor
        if(lastFloorX - transform.position.x < 20) {
            string prefabName;
            lastFloorX += 20;

            if(lastFloorX == RaceControl.instance.goalX) {
                prefabName = "floorLine";
            } else {
                prefabName = "floor";
            }

            GameObject floorObject = ObjectPool.instance.getObjectForType(prefabName);
            floorObject.transform.position = new Vector3(lastFloorX, 0, 0);
        }
    }
}
